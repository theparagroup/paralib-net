using System;


namespace com.paralib.Xandroid
{
    public class ParalibException: Exception
    {
        public ParalibException(string message):base(message)
        {

        }

        public ParalibException(string message, Exception innerException) : base(message,innerException)
        {

        }
    }
}
