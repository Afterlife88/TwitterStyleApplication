using TwitterStyleApplication.Services.ServiceModels.Enums;

namespace TwitterStyleApplication.Services.ServiceModels
{
    /// <summary>
    /// Helper class to validate all stuff on services
    /// </summary>
    public class ServiceState
    {
        private string _errorMessage;

        public ServiceState()
        {
            IsValid = true;
            TypeOfError = TypeOfServiceError.Success;
        }

        public bool IsValid { get; set; }

        public TypeOfServiceError TypeOfError { get; set; }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                IsValid = false;
                _errorMessage = value;
            }
        }
    }
}
