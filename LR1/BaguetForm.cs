using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using WMPLib;
//using BaguetFactory;

namespace BaguetFactory
{
    public partial class BaguetForm : Form
    {
        //main order
        Order order;
        //list of types
        List<Type> list;
        ISerializable<Baguet> serialization;
        Baguet bg = new Baguet();
        string xmlPath = @"D:\BaguetStorage\StorageSerialized.xml";
        string jsonPath = @"D:\BaguetStorage\StorageSerialized.json";

        public BaguetForm()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void BaguetForm_Load(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            trackBar1.Enabled = false;
        }
        

        private void Button1_Click(object sender, EventArgs e)
        {
            //Storage st = new Storage();
            if (radioButton1.Checked)
                order = new Order(Storage.MaterialTakingFromDB);
            else if(radioButton2.Checked)
                order = new Order(Storage.MaterialTakingFromFile);
            list = new List<Type>();
            Type[] materials;
            if (checkBox1.Checked) list.Add(typeof(Wood));
            if (checkBox2.Checked) list.Add(typeof(MetalProfile));
            if (checkBox3.Checked) list.Add(typeof(PlasticProfile));
            if (checkBox4.Checked) list.Add(typeof(Dye));
            if (checkBox5.Checked)
            {
                for (int i = 0; i < trackBar1.Value; i++)
                    list.Add(typeof(Polish));
            }
            materials = list.ToArray();
            try
            {
                //                  ||
                //                  ||
                //                  \/
                bg = order.MakeOrder(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), materials);
                if (order.Cost == -1) MessageBox.Show("Not enough materials in storage");
                else if (order.Cost > 0) MessageBox.Show("Cost of your baguet is " + order.Cost);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.GetType().ToString());
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            //Storage st = new Storage();
            dataGridView1.DataSource = Storage.ShowStorage();
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["Type"].ReadOnly = true;
            dataGridView1.Columns["Amount"].ReadOnly = true;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled == false && checkBox3.Enabled == false)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
            else if (checkBox2.Enabled == true && checkBox3.Enabled == true)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled == false && checkBox3.Enabled == false)
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
            }
            else if (checkBox1.Enabled == true && checkBox3.Enabled == true)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled == false && checkBox2.Enabled == false)
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
            else if (checkBox1.Enabled == true && checkBox2.Enabled == true)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
        }
        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked)
            {
                trackBar1.Enabled = true;
            }
            else
            {
                trackBar1.Enabled = false;
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = String.Format("{0}", trackBar1.Value);
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            //Storage st = new Storage();
            textBox3.Text = Storage.ShowStorage(@"D:\BaguetStorage\");
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                textBox4.Enabled = true;
            else
                textBox4.Enabled = false;
        }
        private void Button4_Click_1(object sender, EventArgs e)
        { 

            //Storage st = new Storage();
            if (checkBox6.Checked)
            {
                Storage.changeInFile(typeof(Wood), Int32.Parse(textBox4.Text));
            }
            if (checkBox7.Checked)
            {
                Storage.changeInFile(typeof(MetalProfile), Int32.Parse(textBox5.Text));
            }
            if (checkBox8.Checked)
            {
                Storage.changeInFile(typeof(PlasticProfile), Int32.Parse(textBox6.Text));
            }
            if (checkBox9.Checked)
            {
                Storage.changeInFile(typeof(Dye), Int32.Parse(textBox7.Text));
            }
            if (checkBox10.Checked)
            {
                Storage.changeInFile(typeof(Polish), Int32.Parse(textBox8.Text));
            }
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
                textBox5.Enabled = true;
            else
                textBox5.Enabled = false;
        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
                textBox6.Enabled = true;
            else
                textBox6.Enabled = false;
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = checkBox9.Checked;
        }

        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            textBox8.Enabled = checkBox10.Checked;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayer a = new WindowsMediaPlayer();

            MessageBox.Show(String.Format("You took {0} material this week", Storage.MaterialShowingPerWeek()));
        }



        private void Button6_Click(object sender, EventArgs e)
        {

            if (radioButton3.Checked)
            {
                serialization = new XmlSerialization<Baguet>(xmlPath, bg);
                if(serialization.Serialize())
                {
                    textBox9.Text = "Object serialized";
                }
            }
            else if (radioButton4.Checked)
            {
                serialization = new JsonSerialization<Baguet>(jsonPath, bg);
                if(serialization.Serialize())
                {
                    textBox9.Text = "Object serialized";
                }
            }           
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            Baguet newBg = serialization.Deserialize();
            string str = String.Format("Object DEserialized: Width: {0}, Height: {1}, Cost: {2}", newBg.Width, newBg.Height, newBg.Cost);
            textBox9.Text = str;
        }
        bool isPlaing = false;

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("some text");
            //aaaaaaaaaaaaaaaaaaaaaaaaa
            //asasdasads
        }
    }
}
