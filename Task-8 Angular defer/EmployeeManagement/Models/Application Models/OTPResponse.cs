namespace EmployeeManagement.Models.Application_Models
{
    public class OTPResponse : IDisposable
    {
        public int OTP { get; set; }
        public string OTPStatus { get; set; }
        public bool isOtpVerified { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.OTP = int.MinValue;
                    this.OTPStatus = string.Empty;
                    this.isOtpVerified = false;
                }

                this.OTP = int.MinValue;
                this.OTPStatus = string.Empty;
                this.isOtpVerified = false;
            }
        }

        ~OTPResponse()
        {
            Dispose(false);
        }
    }
}
