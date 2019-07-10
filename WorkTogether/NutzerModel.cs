using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace WorkTogether
{
    class NutzerModel
    {
        public static string Vorname { get; set; }
        public static string Nachname { get; set; }
        public static string Mail { get; set; }
        public static string Passwort { get; set; }
        public static int? Admin { get; set; }
        public static int? ID { get; set; }
        public static int? PLZ { get; set; }

        public static void readSingleUser(string response)
        {
            //response = response.Replace("null", "empty");
            JsonTextReader reader = new JsonTextReader(new StringReader(response));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    try
                    {
                        if (reader.Value.Equals("Vorname"))
                        {
                            reader.Read();
                            Vorname = reader.Value.ToString();
                        }
                        if (reader.Value.Equals("Nachname"))
                        {
                            reader.Read();
                            Nachname = reader.Value.ToString();
                        }
                        if (reader.Value.Equals("Mail"))
                        {
                            reader.Read();
                            Mail = reader.Value.ToString();
                        }
                        if (reader.Value.Equals("Passwort"))
                        {
                            reader.Read();
                            Passwort = reader.Value.ToString();
                        }
                        if (reader.Value.Equals("Admin"))
                        {
                            reader.Read();
                            Admin = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Value.Equals("ID"))
                        {
                            reader.Read();
                            ID = Convert.ToInt32(reader.Value);
                        }
                        if (reader.Value.Equals("PLZ"))
                        {
                            reader.Read();
                            PLZ = Convert.ToInt32(reader.Value);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        Vorname = null;
                        Nachname = null;
                        Mail = null;
                        Passwort = null;
                        Admin = null;
                        ID = null;
                        PLZ = null;
                        break;
                    }
                }
            }
        }
        public static void saveUser()
        {
            RestClient rClient = new RestClient();
            rClient.httpMethod = httpVerb.POST;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Vorname");
                writer.WriteValue(Vorname);
                writer.WritePropertyName("Nachname");
                writer.WriteValue(Nachname);
                writer.WritePropertyName("Mail");
                writer.WriteValue(Mail);
                writer.WritePropertyName("Passwort");
                writer.WriteValue(Passwort);
                writer.WritePropertyName("Admin");
                writer.WriteValue(Admin.ToString());
                writer.WritePropertyName("ID");
                writer.WriteValue(ID.ToString());
                writer.WritePropertyName("PLZ");
                writer.WriteValue(PLZ.ToString());
                writer.WriteEndObject();
            }
            rClient.postJSON = sb.ToString();
            rClient.endPoint = "http://h2793624.stratoserver.net/php_rest/api/post/create_user.php";
            rClient.makeRequest();
        }
    }
}
