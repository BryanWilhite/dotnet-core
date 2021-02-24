using Newtonsoft.Json.Linq;
using System;

namespace Songhay.AngularForms
{
    public static class JArrayExtensions
    {
        public static JArray AddForm1Configuration(this JArray jArray)
        {
            VerifyArg(jArray);

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

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray AddForm2AreaConfiguration(this JArray jArray)
        {
            VerifyArg(jArray);

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

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray AddForm2PrefixConfiguration(this JArray jArray)
        {
            VerifyArg(jArray);

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

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray AddForm2LineConfiguration(this JArray jArray)
        {
            VerifyArg(jArray);

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

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray AddForm3PasswordConfiguration(this JArray jArray)
        {
            VerifyArg(jArray);

            const string key = "password";

            var anon = new
            {
                key = key,
                type = "input",
                templateOptions = new
                {
                    label =  "Password",
                    minLength =  8,
                    pattern = "^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$",
                    required = true,
                    type = key
                }
            };

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray AddForm3AgeConfiguration(this JArray jArray)
        {
            VerifyArg(jArray);

            var anon = new
            {
                key = "age",
                type = "input",
                templateOptions = new
                {
                    label =  "Age",
                    max = 65,
                    min = 18,
                    minLength =  8,
                    required = true,
                    type = "number"
                }
            };

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray FillWithForm2Configuration(this JArray jArray)
        {
            VerifyArg(jArray);

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
                    fieldGroup = JArray.Parse("[]")
                        .AddForm2AreaConfiguration()
                        .AddForm2PrefixConfiguration()
                        .AddForm2LineConfiguration()
                }
            };

            jArray.Add(JObject.FromObject(anon));

            return jArray;
        }

        public static JArray FillWithForm3Configuration(this JArray jArray)
        {
            VerifyArg(jArray);

            jArray
                .AddForm3PasswordConfiguration()
                .AddForm3AgeConfiguration();

            return jArray;
        }

        private static void VerifyArg(JArray jArray)
        {
            if(jArray == null) throw new ArgumentNullException($"The expected {nameof(JArray)} is not here.");
        }
    }
}