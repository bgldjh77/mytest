using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.SqlFunc
{
    class GetUser
    {

        //获取上次登录
        public static Dictionary<string, object> getlastlogin()
        {
            SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
            List<Dictionary<string, object>> articles = dbHelper.GetRowDatas($"select * from (select * from user_account order by lastlogintime desc) limit 1");
            dbHelper.Close();
            if (articles == null) { return null; }

            foreach (var article in articles)
            {
                return article;
            }
            return null;
        }
        //获取密码
        public static string getuserpassword(string phone)
        {
            // 创建SQLiteHelper实例
            SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
            List<Dictionary<string, object>> articles = dbHelper.GetRowDatas($"SELECT password FROM user_account where phone='{phone}'");//获取name对应密码
            dbHelper.Close();
            foreach (var article in articles)
            {
                Console.WriteLine(article["password"]);
                return article["password"].ToString();
            }

            return "";
        }
        //更新登录数据
        public static bool updatelogin(string phone, string password, string remindpass )
        {
            Clear_fragment();
            if (getuserpassword(phone)== password)
            {
                string logintime = GetNowTime.nowtime2().Replace("/", "").Replace(" ", "").Replace(":", "").Replace("-","");
                SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
                dbHelper.ExecuteNonQuery($"update user_account set lastlogintime = '{logintime}', remindpassword = '{remindpass}' where phone = '{phone}'");
                dbHelper.Close();
                return true;
            }

            return false;
        }

        //获取用户列表
        public static List<string> getuserlist()
        {
            // 创建SQLiteHelper实例
            SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
            List<string> ls = new List<string>();
            List<Dictionary<string, object>> res = dbHelper.GetRowDatas("SELECT phone FROM user_account");//获取phone列
            dbHelper.Close();
            foreach (var result in res)
            {
                ls.Add(result["phone"].ToString());
            }

            return ls;
        }
        //是否存在该用户
        public static bool hasaccount(string phone)
        {
            List<string> accountlist = getuserlist();
            if (accountlist.Contains(phone))
            {
                return true;
            }
            return false;
        }

        //整理数据库碎片
        public static void Clear_fragment()
        {
            //整理数据库碎片
            SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
            dbHelper.VACUUM();
        }
    }
}
