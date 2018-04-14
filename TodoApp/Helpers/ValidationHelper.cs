namespace TodoApp.Helpers
{
    internal class ValidationHelper
    {
        private static ValidationHelper _instance;

        protected ValidationHelper()
        {
        }

        public static ValidationHelper Instance()
        {
            if (_instance == null)
            {
                _instance = new ValidationHelper();
            }
            return _instance;
        }

        /// <summary>
        /// Metod for checking if object is null.
        /// </summary>
        /// <param name="obj">Object to be evaluated.</param>
        /// <returns>T if object is null, F if not.</returns>
        public bool IsObjectNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for validating if string is null or empty.
        /// </summary>
        /// <param name="str">String to be evaluated.</param>
        /// <returns>T if string is null or empty, F if not.</returns>
        public bool ValidateString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            return false;
        }
    }
}