using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SqlFunc;
using 质检工具.User.UserConfig;

namespace 质检工具
{
    public partial class ResignForm : AntdUI.Window
    {
        public ResignForm()
        {
            InitializeComponent();
        }

        private void ResignForm_Load(object sender, EventArgs e)
        {
            getusercollection();
            input1.Text = "";
            input2.Text = "";
            select1.SelectedIndex = 0;
        }
        private void getusercollection()
        {
            foreach (string name in Enum.GetNames(typeof(UserTypeEnum)))
            {
                select1.Items.Add((name));
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string phone = input1.Text;
            string password = input2.Text;
            string usertype = select1.Text;
            string noticemessage = "\n注册失败";
            if (!string.IsNullOrWhiteSpace(phone)&& !string.IsNullOrWhiteSpace(password))
            {
                if (GetUser.hasaccount(phone))
                {
                    noticemessage= "\n注册失败，用户已存在";
                }
                else
                {
                    SQLiteHelper dbHelper = new SQLiteHelper(globalpara.dbpath);
                    int res = dbHelper.ExecuteNonQuery($"INSERT INTO user_account( phone, password,usertype)VALUES('{phone}', '{password}', " +
                        $" '{usertype}'); ");
                    if(res== 1)
                    {
                        dbHelper.Close();
                        noticemessage = "\n注册成功";
                        this.Close();
                    }
                    else { noticemessage = "\n注册失败，数据库未知错误"; }
                }                   
            }
            AntdUI.Modal.open(new AntdUI.Modal.Config(this, "注册", noticemessage, AntdUI.TType.None)
            {
                OnButtonStyle = (id, btn) =>
                {
                    btn.BackExtend = "135, #6253E1, #04BEFE";
                },
                CancelText = null,
                OkText = "知道了"
            });
        }

        
    }
}
