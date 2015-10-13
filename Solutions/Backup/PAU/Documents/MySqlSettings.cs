using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace PAU.Documents
{
    [DataContract]
    public class MySqlSettings
    {
        public string ConnectionString(string server, string username, string password, string dbname)
        {
            return string.Format("Server={0};User={1};Password={2};database={3}", server, username, password, dbname);
        }

        public string ConnectionString()
        {
            return ConnectionString(Server, UserName, Password, Database);
        }

        public static void Serialize(string filename, MySqlSettings settings)
        {
            XmlWriter xw = null;

            try
            {
                XmlWriterSettings xset = new XmlWriterSettings();
                xset.Indent = true;
                xset.NewLineOnAttributes = true;
                xw = XmlWriter.Create(filename, xset);

                DataContractSerializer dcs =
                    new DataContractSerializer(typeof(MySqlSettings));
                dcs.WriteObject(xw, settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xw != null)
                {
                    xw.Flush();
                    xw.Close();
                }
            }
        }

        public static void Serialize(string filename)
        {
            Serialize(filename, MySqlSettings.Instance);
        }

        public static MySqlSettings Deserialize(string filename)
        {
            MySqlSettings settings = null;
            FileStream fs = null;
            XmlReader xr = null;

            try
            {
                fs = new FileStream(filename, FileMode.Open);
                xr = XmlReader.Create(fs);

                DataContractSerializer dcs =
                    new DataContractSerializer(typeof(MySqlSettings));

                settings = (MySqlSettings)dcs.ReadObject(xr, true);
            }
            catch (Exception)
            {
                //throw ex;
            }
            finally
            {
                if (xr != null) xr.Close();
                if (fs != null) fs.Close();
            }

            return settings;
        }

        public static void DeserializeInstance(string filename)
        {
            if (File.Exists(filename))
            {
                MySqlSettings settings = Deserialize(filename);
                if (settings != null)
                {
                    _instance = settings;
                }
            }
        }

        private static MySqlSettings _instance = null;
        public static MySqlSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MySqlSettings();
                }

                return _instance;
            }
        }

        private string _server = "127.0.0.1";
        [DataMember]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }


        private string _username = "root";
        [DataMember]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password = "";
        [DataMember]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _dbname = "beacukai";
        [DataMember]
        public string Database
        {
            get { return _dbname; }
            set { _dbname = value; }
        }
    }
}
