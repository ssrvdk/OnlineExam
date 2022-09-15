using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ExceptionManager
{
    public class ServiceErrorBehaviourAttribute : Attribute, IErrorHandler, IServiceBehavior
    {
        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            //ExceptionPolicy.HandleException(error, ExceptionPolicyName.ServiceInterface);
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            BusinessLogicException ble = error as BusinessLogicException;
            ValidationException ve = error as ValidationException;
            if (ble != null)
            {
                FaultException<BusinessError[]> feb = new FaultException<BusinessError[]>(ble.BusinessErrors.ToArray(), "Business logic error");
                fault = Message.CreateMessage(version, feb.CreateMessageFault(), null);
            }
            else if (ve != null)
            {
                FaultException<BusinessError[]> fev = new FaultException<BusinessError[]>(ve.ValidationErrors.ToArray(), "Validation error");
                fault = Message.CreateMessage(version, fev.CreateMessageFault(), null);
            }
            else if (!(error is FaultException))
            {
                TechnicalError te = new TechnicalError();
                te.ErrorId = Guid.NewGuid();
                //FaultException<TechnicalError> fet = new FaultException<TechnicalError>(te, "Technical Error:" + te.ErrorId);
                //# Warning "Before deployment this must change to Error Id instead of Error Message"
                //FaultException<TechnicalError> fet = new FaultException<TechnicalError>(te, te.ErrorId.ToString());
                FaultException<TechnicalError> fet = new FaultException<TechnicalError>(te, error.Message.ToString());
                fault = Message.CreateMessage(version, fet.CreateMessageFault(), null);
                try
                {
                    TraceSource ts = new TraceSource("LBLoggingService");
                    ts.TraceEvent(TraceEventType.Error, 0, "Error occured for Guid " + te.ErrorId + " And Error Message is " + error.Message);
                }
                catch (Exception ex)
                {
                }
                //end of the modification            
            }
        }
        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Console.WriteLine("The ServiceErrorBehaviourAttribute has been applied.");
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(this);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Console.WriteLine("Validate is called.");
            foreach (ServiceEndpoint se in serviceDescription.Endpoints)
            {
                // Must not examine any metadata endpoint.
                if (se.Contract.Name.Equals("IMetadataExchange") && se.Contract.Namespace.Equals("http://schemas.microsoft.com/2006/04/mex"))
                    continue;

                foreach (OperationDescription opDesc in se.Contract.Operations)
                {
                    bool businessFaultExists = false;
                    bool technicalFaultExists = false;
                    foreach (FaultDescription fault in opDesc.Faults)
                    {
                        if (fault.DetailType.Equals(typeof(BusinessError[])))
                        {
                            businessFaultExists = true;
                            continue;
                        }
                        if (fault.DetailType.Equals(typeof(TechnicalError)))
                        {
                            technicalFaultExists = true;
                            continue;
                        }
                    }
                    if (!businessFaultExists || !technicalFaultExists)
                    {
                        throw new InvalidOperationException("ServiceErrorBehaviour requires BusinessError[] and TechnicalError fault contracts on " + opDesc.Name);
                    }
                }
            }
        }
        #endregion
    }
}
