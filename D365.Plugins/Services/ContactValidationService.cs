using D365.Plugins.Abstracts;
using Microsoft.Xrm.Sdk;
using System;

namespace D365.Plugins.Services
{
    public class ContactValidationService : BasePluginService
    {
        public ContactValidationService(Lazy<IOrganizationServiceFactory> organizationServiceFactory, ITracingService tracingService) : base(organizationServiceFactory, tracingService)
        {
        }

        public override void Execute(IPluginExecutionContext executionContext)
        {
            if (executionContext.InputParameters.Contains("Target")){
                Entity contactEntity = (Entity)executionContext.InputParameters["Target"];
                Validate(contactEntity);
            } 
        }

        protected void Validate(Entity contactEntity)
        {
            bool isValid = false;
            if (contactEntity.Contains("firstname"))
            {
                isValid = ValidateFirstName(contactEntity.GetAttributeValue<string>("firstname"));

                if (!isValid)
                {
                    throw new InvalidPluginExecutionException("First name does not pass validation. First name cannot include digits.");
                }
            }

            if (contactEntity.Contains("lastname"))
            {
                isValid = ValidateLastName(contactEntity.GetAttributeValue<string>("lastname"));

                if (!isValid)
                {
                    throw new InvalidPluginExecutionException("Last name does not pass validation. Last name cannot include digits.");
                }
            }

            if (contactEntity.Contains("telephone1"))
            {

                isValid = ValidatePhoneNumber(contactEntity.GetAttributeValue<string>("telephone1"));

                if (!isValid)
                {
                    throw new InvalidPluginExecutionException("Phone number does not pass validation.");
                }
            }
        }

        protected bool ValidateFirstName(string firstName)
        {
            foreach (char c in firstName)
            {
                if (char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ValidateLastName(string lastName)
        {
            foreach (char c in lastName)
            {
                if (char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("+48"))
            {
                return true;
            }
            return false;
        }
    }
}
