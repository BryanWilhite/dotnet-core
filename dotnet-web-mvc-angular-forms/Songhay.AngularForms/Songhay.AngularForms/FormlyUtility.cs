using System;

namespace Songhay.AngularForms
{
    public static class FormlyUtility
    {
        public static object[] GetForm1Configuration()
        {
            const string key = "email";

            var anon = new
            {
                key = key,
                type = "input",
                templateOptions = new
                {
                    label = key,
                    placeholder = $"enter {key}",
                    required = true,
                    type = key
                }
            };

            return new object[] { anon };
        }

        public static object GetForm2AreaConfiguration()
        {
            const string key = "area";

            var anon = new
            {
                className = "col-sm-3",
                key = key,
                type = "input",
                templateOptions = new
                {
                    maxLength = 3,
                    placeholder = key,
                    required = true,
                    type = "text"
                }
            };

            return anon;
        }

        public static object[] GetForm2Configuration()
        {
            var anon = new
            {
                key = "phones",
                type = "repeat-phone",
                templateOptions = new
                {
                    addText = "Add Phone Number"
                },
                fieldArray = new
                {
                    fieldGroupClassName = "phones row",
                    fieldGroup = new object[] {
                        GetForm2AreaConfiguration(),
                        GetForm2PrefixConfiguration(),
                        GetForm2LineConfiguration(),
                     }
                }
            };

            return new object[] { anon };
        }

        public static object GetForm2LineConfiguration()
        {
            const string key = "line";

            var anon = new
            {
                className = "col-sm-6",
                key = key,
                type = "input",
                templateOptions = new
                {
                    maxLength = 4,
                    placeholder = key,
                    required = true,
                    type = "text"
                }
            };

            return anon;
        }

        public static object GetForm2PrefixConfiguration()
        {
            const string key = "prefix";

            var anon = new
            {
                className = "col-sm-3",
                key = key,
                type = "input",
                templateOptions = new
                {
                    maxLength = 3,
                    placeholder = key,
                    required = true,
                    type = "text"
                }
            };

            return anon;
        }

        public static object GetForm3AgeConfiguration()
        {
            var anon = new
            {
                key = "age",
                type = "input",
                templateOptions = new
                {
                    label = "Age",
                    max = 65,
                    min = 18,
                    minLength = 8,
                    required = true,
                    type = "number"
                }
            };

            return anon;
        }

        public static object[] GetForm3Configuration()
        {
            return new object[]
            {
                GetForm3PasswordConfiguration(),
                GetForm3AgeConfiguration(),
            };
        }

        public static object GetForm3PasswordConfiguration()
        {
            const string key = "password";

            var anon = new
            {
                key = key,
                type = "input",
                templateOptions = new
                {
                    label = "Password",
                    minLength = 8,
                    pattern = "^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$",
                    required = true,
                    type = key
                }
            };

            return anon;
        }
    }
}