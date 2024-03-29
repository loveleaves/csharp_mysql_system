﻿using System;
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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }
        
         
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            sign stu = new sign();
            stu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 连接数据库
            //定义连接字符串
            string connStr = "Database=StudentStatusManagement;Data Source=127.0.0.1;port=3306;User Id=root;password=iloveyou";
            MySqlConnection conn = new MySqlConnection(connStr);//创建Connection对象
            conn.Open();//打开数据库
            #endregion
            if (userName.Text=="")
            {
                MessageBox.Show("用户名不能为空", "用户登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else if (passWord.Text == "")
            {
                MessageBox.Show("密码不能为空", "用户登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (verification.Text == "")
            {
                MessageBox.Show("验证码不能为空", "用户登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (verification.Text != label4.Text)
            {
                MessageBox.Show("验证码错误，请重新输入", "用户登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                // 查询数据库账户信息
                string username = userName.Text;
                string password = passWord.Text;
                string sql1 = "SELECT COUNT(*) FROM USER WHERE userName='" + username + "'; ";
                string sql2 = "SELECT COUNT(*) FROM USER WHERE userName='" + username + "' AND userPassword = '" + password + "'; ";
                
                try
                {
                    MySqlCommand com1 = new MySqlCommand(sql1, conn);
                    if (Convert.ToInt32(com1.ExecuteScalar()) == 0)
                    {
                        MessageBox.Show("该用户不存在！", "用户登陆", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        // sql查询处理，防sql注入
                        try
                        {
                            MySqlCommand com2 = new MySqlCommand(sql2, conn);
                            if (Convert.ToInt32(com2.ExecuteScalar()) == 0)
                            {
                                MessageBox.Show("密码错误，请重新输入！", "用户登录", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                            {
                                Menu stu = new Menu();
                                stu.Show();
                                this.Hide();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("密码含有敏感字符！", "用户登陆", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("用户名含有敏感字符！", "用户登陆", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void register_Load(object sender, EventArgs e)
        {
            Random rd = new Random();
            label4.Text = Convert.ToString(rd.Next(1000, 10000));
        }
    }
}
