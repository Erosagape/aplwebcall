using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
namespace WebCall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //set ให้ .net รองรับ https ด้วย protocol tls 12
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //เครียมข้อมูลส่งเข้า API
                CRequestLogin data = new CRequestLogin
                {
                    userName = "akarat",
                    systemCode = "Admin1111*"
                };
                string postData = JsonConvert.SerializeObject(data);
                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                
                //เชื่อมต่อ API
                WebRequest req = WebRequest.Create("https://sg5.fusionsolar.huawei.com/thirdData/login");
                
                //กำหนด header parameters
                req.Method = "POST";
                req.ContentLength = postBytes.Length;
                req.ContentType = "application/json";
                
                //ส่งค่า json ไปเป็น content
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(postBytes, 0, postBytes.Length);
                dataStream.Close();
                
                //รอรับ response
                WebResponse res = req.GetResponse();
                using (dataStream = res.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string resData = reader.ReadToEnd();
                    
                    //แสดงผล response
                    textBox1.Text = resData;
                    
                    //อ่านค่า object เป็นแบบ dynamic 
                    var resObj = JsonConvert.DeserializeObject<dynamic>(resData);
                    
                    //อ่านค่า field ต่างๆ 
                    var _success = Convert.ToString(resObj.success);
                    var _param= Convert.ToString(resObj["params"]);
                    var _message = Convert.ToString(resObj.message);
                    var _data = Convert.ToString(resObj.data);
                    var _failCode=Convert.ToString(resObj.failCode);

                    //แสดง output ค่าที่อ่านมาได้
                    var str = "Success:" + _success + "\n";
                    str += "params:" + _param + "\n";
                    str += "message:" + _message + "\n";
                    str += "data:" + _data + "\n";
                    str += "failCode:" + _failCode + "\n";
                    
                    MessageBox.Show(str);
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //set ให้ .net รองรับ https ด้วย protocol tls 12
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                byte[] postBytes = Encoding.UTF8.GetBytes("{}");

                //เชื่อมต่อ API
                WebRequest req = WebRequest.Create("https://sg5.fusionsolar.huawei.com/thirdData/getStationList");

                //กำหนด header parameters
                req.Method = "POST";
                req.ContentLength = postBytes.Length;
                req.ContentType = "application/json";

                //ส่งค่า json ไปเป็น content
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(postBytes, 0, postBytes.Length);
                dataStream.Close();

                //รอรับ response
                WebResponse res = req.GetResponse();
                using (dataStream = res.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string resData = reader.ReadToEnd();

                    //แสดงผล response
                    textBox1.Text = resData;
                    /*
                    //อ่านค่า object เป็นแบบ dynamic 
                    var resObj = JsonConvert.DeserializeObject<dynamic>(resData);

                    //อ่านค่า field ต่างๆ 
                    var _success = Convert.ToString(resObj.success);
                    var _param = Convert.ToString(resObj["params"]);
                    var _message = Convert.ToString(resObj.message);
                    var _data = Convert.ToString(resObj.data);
                    var _failCode = Convert.ToString(resObj.failCode);

                    //แสดง output ค่าที่อ่านมาได้
                    var str = "Success:" + _success + "\n";
                    str += "params:" + _param + "\n";
                    str += "message:" + _message + "\n";
                    str += "data:" + _data + "\n";
                    str += "failCode:" + _failCode + "\n";

                    MessageBox.Show(str);
                    */
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

        }
    }
}
