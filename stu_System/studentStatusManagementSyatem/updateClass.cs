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
    public partial class updateClass : Form
    {
        public updateClass()
        {
            InitializeComponent();
        }

        private void updateClass_Load(object sender, EventArgs e)
        {
            classId.Text = BrowseClass.t1;
            className.Text = BrowseClass.t2;
            count.Text = BrowseClass.t3;
            emplain.Text = BrowseClass.t4;
            teacher.Text = BrowseClass.t5;
            #region 连接数据库
            //定义连接字符串
            string connStr = "Database=StudentStatusManagement;Data Source=127.0.0.1;port=3306;User Id=root;password=iloveyou;";
            MySqlConnection conn = new MySqlConnection(connStr);//创建Connection对象
            conn.Open();//打开数据库
            #endregion
            //导入专业进集合

            MySqlCommand st = new MySqlCommand("select majorName from major", conn);
            MySqlDataReader reader1 = st.ExecuteReader();
            major.Items.Clear();
            while (reader1.Read())
            {
                major.Items.Add((string)reader1[0]);
            }
            reader1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 连接数据库
            //定义连接字符串
            string connStr = "Database=StudentStatusManagement;Data Source=127.0.0.1;port=3306;User Id=root;password=iloveyou;";
            MySqlConnection conn = new MySqlConnection(connStr);//创建Connection对象
            conn.Open();//打开数据库
            #endregion
            string banjiId = classId.Text;
            string banjiName = className.Text;
            string banzhur = teacher.Text;
            string renshu = count.Text;

            //获得专业名称
            string zhuanye = "";
            if (major.SelectedIndex != -1)
            {
                zhuanye = major.SelectedItem.ToString();
            }
            else
            {
                major.SelectedIndex = 0;
            }
            //获得专业id
            string zhuanyeId = "";
            string sql = "select majorId from major where majorName='" + zhuanye + "'";
            MySqlCommand comt = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comt.ExecuteReader();
            while (reader.Read())
            {
                zhuanyeId = (string)reader[0];
            }
            reader.Close();
            string shuoming = emplain.Text;
            string sql2 = "update Class set className='" + banjiName + "',classCount='" + renshu + "',teacher='" + banzhur + "',classExplain='" + shuoming + "',majorId='" + zhuanyeId + "' where ClassId='"+ banjiId + "'";
            MySqlCommand com2 = new MySqlCommand(sql2, conn);
            com2.ExecuteNonQuery();
             MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Hide();
            
        }
    }
}
