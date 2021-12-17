using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace studentStatusManagementSyatem
{
    public partial class sign : Form
    {
        public sign()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 连接数据库
            //定义连接字符串
            string connStr = "Database=StudentStatusManagement;Data Source=127.0.0.1;port=3306;User Id=root;password=iloveyou;";
            MySqlConnection conn = new MySqlConnection(connStr);//创建Connection对象
            conn.Open();//打开数据库
            #endregion
            if (userName.Text == "")
            {
                MessageBox.Show("用户名不能为空", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else if (passWord.Text == "")
            {
                MessageBox.Show("密码不能为空", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }else if (againPassword.Text == "")
            {
                MessageBox.Show("请再次输入密码", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }else if (passWord.Text != againPassword.Text)
            {
                MessageBox.Show("两次密码输入不一致", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string username = userName.Text;
                string password = passWord.Text;
                //账户存入数据库
                string sql1 = "SELECT COUNT(*) FROM USER WHERE userName='" + username + "'; ";
                string sql2 = "insert into User set userName='" + username + "',userPassword='" + password + "'";
                // 禁止用户名一样
                try
                {
                    MySqlCommand com1 = new MySqlCommand(sql1, conn);
                    if (Convert.ToInt32(com1.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("用户名已存在！", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        // sql查询处理，防sql注入
                        try
                        {
                            MySqlCommand com2 = new MySqlCommand(sql2, conn);
                            com2.ExecuteNonQuery();
                            MessageBox.Show("注册成功", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Hide();
                            register stu = new register();
                            stu.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("密码含有敏感字符！", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户名含有敏感字符！", "用户注册", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            register stu = new register();
            stu.Show();
        }

        private void userName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
