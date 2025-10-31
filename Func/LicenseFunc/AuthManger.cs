using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using 质检工具.User.UserConfig;

namespace 质检工具.Func.LicenseFunc
{
    class AuthManger
    {
        public static string m_licFullDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.zjzxzj";
        static string m_licFullName = m_licFullDirectory + "\\Licence.lic";
        static string licSuffixName = "Licence.lic";
        static string machine_code = getMachineCode();
        
        public static string GetLicValue()
        {
            //user_account db = new user_account();
            UserBean user = LoginForm.userBean;
            string licDesName = m_licFullDirectory + "\\" + user.phone + licSuffixName;
            try
            {
                return File.ReadAllText(licDesName);
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 验证加载许可文件，如果通过，再替换本地许可文件
        /// </summary>
        /// <param name="licFullName"></param>
        /// <returns></returns>
        public static bool Verify(string licFullName)
        {
            if (!File.Exists(licFullName)) return false;
            byte[] lic = File.ReadAllBytes(licFullName);

            //user_account db = new user_account();
            UserBean user = LoginForm.userBean;
            string licDesName = m_licFullDirectory + "\\" + user.phone + licSuffixName;

            if (Verify(lic))
            {
                try
                {
                    File.Delete(licDesName);
                }
                catch (Exception e)
                {
                }
                Directory.CreateDirectory(m_licFullDirectory);
                File.Copy(licFullName, licDesName);
                return true;
            }
            return false;
        }
        public static bool Verify()
        {
            //user_account db = new user_account();
            UserBean user = LoginForm.userBean;
            string licDesName = m_licFullDirectory + "\\" + user.phone + licSuffixName;
            if (File.Exists(licDesName))
            {
                byte[] lic = File.ReadAllBytes(licDesName);
                return Verify(lic);
            }
            else
            {
                return false;
            }

        }

        public static bool Verify(byte[] licBytes)
        {
            try
            {
                string strlic = System.Text.Encoding.UTF8.GetString(licBytes);
                string decryptStr = DecryptDES(strlic, "ZJZXZJRJ").Trim();
                return (GetMachineCode() + "@" + GetEncryUserInfo()).Equals(decryptStr);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static string GetMachineCode()
        {
            return machine_code;
        }
        public static string GetEncryUserInfo()
        {
            //user_account db = new user_account();
            UserBean user = LoginForm.userBean;
            return EncryptDES(user.name + "@" + user.phone + "@" + user.type);
        }
        //加密
        public static string EncryptDES(string plainText, string key = "ZJZXZJRJ")
        {
            byte[] iv = new byte[8];
            byte[] array;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = iv;

                ICryptoTransform cryptoTransform = des.CreateEncryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        array = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(array, 0, array.Length);
                        cryptoStream.FlushFinalBlock();
                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        //脱密
        public static string DecryptDES(string cipherText, string key = "ZJZXZJRJ")
        {
            byte[] iv = new byte[8];
            byte[] array = Convert.FromBase64String(cipherText);

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = iv;

                ICryptoTransform cryptoTransform = des.CreateDecryptor();

                using (MemoryStream memoryStream = new MemoryStream(array))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        //获取机器码
        private static string getMachineCode()
        {
            string machineCodeString = string.Empty;
            machineCodeString = getCpuInfo() + "@" + getHDid();
            return machineCodeString;
        }
        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns></returns>
        private static string getCpuInfo()
        {
            string cpuInfo = "";
            try
            {
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cpuInfo.ToString();
        }

        ///   <summary> 
        ///   获取硬盘ID     
        ///   </summary> 
        ///   <returns> string </returns> 
        private static string getHDid()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }
    }
}
