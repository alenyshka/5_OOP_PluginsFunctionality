using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XML.serializaton.Factories;
using XML.serializaton.Decorations;
using System.Xml.Serialization;
using System.Reflection;
using Newtonsoft.Json;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml;

namespace XML.serializaton
{

    public partial class Form1 : Form
    {
        public List<Factory> factory = new List<Factory>();
        public List<Type> extraTypes = new List<Type>();
        public List<DecorationClass> labels = new List<DecorationClass>();
       
        List<DecorationClass> DecorationList = new List<DecorationClass>();        
        List<string> FieldList = new List<string>();
        List<TextBox> TextBoxList = new List<TextBox>();
        int i;
        bool flagEdit = false, flagDelete = false;
        List<string> FileNames = new List<string>();
        List<string> ListLoadPlugins = new List<string>();

        public Form1()
        {
            InitializeComponent();

            InitializeFabrika();
            InitializeTypes();
            InitializeLabels();
            LoadPluginsInList(Constants.PluginsPath);
           
            TextBoxList.Add(textBox1);
            TextBoxList.Add(textBox2);
            TextBoxList.Add(textBox3);
            TextBoxList.Add(textBox4);
            TextBoxList.Add(textBox5);  
        }
        void LoadPluginsInList(string path)
        {
            comboBox2.Items.Clear();
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
                int length = path.Length;
                foreach (string dllFile in dllFileNames)
                {
                    string item = dllFile.Substring(length);
                    comboBox2.Items.Add(item);
                }
                textBoxInfo2.Text = "Plugins exist.";
            }
            else
                textBoxInfo2.Text = "Plugins don't exist.";
        }

        void InitializeFabrika()
        {
            factory.Add(new EarringsFactory());
            factory.Add(new RingFactory());
            factory.Add(new ChainFactory());
            factory.Add(new CoulombFactory());
            factory.Add(new WatchesFactory());
            factory.Add(new PinFactory());
        }
        void InitializeTypes()
        {
            extraTypes.Add(typeof(Earrings));
            extraTypes.Add(typeof(Ring));
            extraTypes.Add(typeof(Chain));
            extraTypes.Add(typeof(Coulomb));
            extraTypes.Add(typeof(Watches));
            extraTypes.Add(typeof(Pin));
        }

        void InitializeLabels()
        {
            labels.Add(new Earrings());
            labels.Add(new Ring());
            labels.Add(new Chain());
            labels.Add(new Coulomb());
            labels.Add(new Watches());
            labels.Add(new Pin());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((DecorationList.Count > 0) && (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                FileStream file = new System.IO.FileStream(openFileDialog1.FileName, FileMode.Create);
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DecorationClass>), extraTypes.ToArray());

                    MemoryStream stream = new MemoryStream();
                    xmlSerializer.Serialize(stream, DecorationList);

                    if (checkBox1.Checked)
                    {
                        SerializeDataInToJson(file, stream);
                        textBoxInfo.Text = "Serialization data format JSON completed successfully.";
                    }
                    else
                    {
                        stream.WriteTo(file);
                        stream.Close();
                        file.Close();
                        textBoxInfo.Text = "Serialization data format XML completed successfully.";
                    }               
                }
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }             
            }
            else
            {
                textBoxInfo.Text = "The list doesn't contain objects\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DecorationList.Clear();
                    FileStream reader = new System.IO.FileStream(openFileDialog1.FileName, FileMode.Open);
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DecorationClass>), extraTypes.ToArray());
                        List<DecorationClass> Decoration = new List<DecorationClass>();

                        if (checkBox1.Checked)
                        {
                            try
                            {
                                Decoration = DeserializeDataFromJson(Decoration, reader,  xmlSerializer);
                                textBoxInfo.Text = "Deserialization data from format JSON completed successfully.";
                            }
                            catch
                            {
                                textBoxInfo.Text = "Deserialization data from format JSON completed unsuccessfully.";
                            }
                            
                        }
                        else
                        {
                            Decoration = (List<DecorationClass>)xmlSerializer.Deserialize(reader);
                            textBoxInfo.Text = "Deserialization data from format XML completed successfully.";
                        }
                        reader.Close();
                        DecorationList.Clear();
                        listBox1.Items.Clear();
                        foreach (DecorationClass element in Decoration)
                        {
                            listBox1.Items.Add(enter_item(Convert.ToString(element.Object), Convert.ToString(element.Name),
                            Convert.ToString(element.Weight), Convert.ToString(element.Material)));
                            DecorationList.Add(element);
                        }
                    }
                    finally
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch
            {
                textBoxInfo.Text = "Check the file.\r\nTry to deserialize data in a different format.";
            }  
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labels[comboBox1.SelectedIndex].SetLabels(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FieldList.Clear();
                if(CheckInput())
                {
                    FieldList.Add(comboBox1.GetItemText(comboBox1.SelectedItem));
                    for (i = 1; i <= TextBoxList.Count; i++)
                       if (TextBoxList[i - 1].Text != "")
                            FieldList.Add(TextBoxList[i - 1].Text);
                    if ((radioButton1.Visible == true) && (radioButton2.Visible == true) && ((radioButton1.Checked == true) || (radioButton2.Checked == true)))
                    {
                        if (radioButton1.Checked == true)
                            FieldList.Add(radioButton1.Text);
                        if (radioButton2.Checked == true)
                            FieldList.Add(radioButton2.Text);
                    }
                    DecorationClass decoration = factory[comboBox1.SelectedIndex].FactoryMethod();
                    decoration.SetValues(FieldList);
                    DecorationList.Add(decoration);
                    listBox1.Items.Add(enter_item(Convert.ToString(decoration.Object), Convert.ToString(decoration.Name),
                        Convert.ToString(decoration.Weight), Convert.ToString(decoration.Material)));
                    textBoxInfo.Text = "The object added.\r\n";
                }

            }
            catch 
            {
                textBoxInfo.Text = "The object isn't added.\r\n";
            }
        }

        void SerializeDataInToJson(FileStream file, MemoryStream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.ASCII.GetString(stream.ToArray()));
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(jsonText);
            writer.Flush();
            writer.Close();
        }
        List<DecorationClass> DeserializeDataFromJson(List<DecorationClass> Decoration, FileStream reader, XmlSerializer xmlSerializer)
        {
            string content;

            content = new StreamReader(reader, Encoding.ASCII).ReadToEnd();


            XmlDocument doc = JsonConvert.DeserializeXmlNode(content);
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;
            Decoration = (List<DecorationClass>)xmlSerializer.Deserialize(stream);
            stream.Close();
            return Decoration;
        }
        bool CheckInput()
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != ""))
            {
                if ((textBox5.Visible == true) && (textBox5.Text != ""))
                    return true;
                else if ((radioButton1.Visible == true) && (radioButton2.Visible == true) && ((radioButton1.Checked == true) || (radioButton2.Checked == true)))
                    return true;
                else
                    return false;
            }
            else
            {
                textBoxInfo.Text = "Uncorrected input.";
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    DecorationList.Remove(DecorationList[listBox1.SelectedIndex]);
                    flagDelete = true;
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    flagDelete = false;
                    textBoxInfo.Text = "Object deleted.\r\n";
                }
                else
                    textBoxInfo.Text = "Choose the object.\r\n";
            }
            catch
            {
                textBoxInfo.Text = "Choose the object.\r\n";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FieldList.Clear();
                if ((!flagEdit) && (!flagDelete))
                {
                    comboBox1.Text = DecorationList[listBox1.SelectedIndex].Object;
                    DecorationList[listBox1.SelectedIndex].GetValues(FieldList);
                    for (i = 0; i < FieldList.Count-1; i++)
                        TextBoxList[i].Text = FieldList[i+1];
                    if ((radioButton1.Visible == true) && (radioButton2.Visible == true))
                    {
                        if (Convert.ToString(FieldList[i]) == "True")
                        {
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                        }
                        if (Convert.ToString(FieldList[i]) == "False")
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = true;
                        }
                    }
               }
            }
            catch
            {
                textBoxInfo.Text = "You didn't choose the object.\r\n";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (CheckInput())
                {
                    FieldList.Clear();
                    FieldList.Add(comboBox1.GetItemText(comboBox1.SelectedItem));
                    for (i = 1; i <= TextBoxList.Count - 1; i++)
                        if (TextBoxList[i - 1].Text != "")
                            FieldList.Add(TextBoxList[i - 1].Text);
                    if ((radioButton1.Visible == true) && (radioButton2.Visible == true) && ((radioButton1.Checked == true) || (radioButton2.Checked == true)))
                    {
                        if (radioButton1.Checked == true)
                            FieldList.Add(radioButton1.Text);
                        if (radioButton2.Checked == true)
                            FieldList.Add(radioButton2.Text);
                    }
                    DecorationList[listBox1.SelectedIndex].Object = comboBox1.GetItemText(comboBox1.SelectedItem);
                    DecorationList[listBox1.SelectedIndex].SetValues(FieldList);
                    flagEdit = true;
                    listBox1.Items[listBox1.SelectedIndex] = enter_item(
                        Convert.ToString(DecorationList[listBox1.SelectedIndex].Object),
                        Convert.ToString(DecorationList[listBox1.SelectedIndex].Name),
                        Convert.ToString(DecorationList[listBox1.SelectedIndex].Weight),
                        Convert.ToString(DecorationList[listBox1.SelectedIndex].Material)
                        );
                    flagEdit = false;
                    textBoxInfo.Text = "The object edited.\r\n";
                }
                else
                {
                    textBoxInfo.Text = "Check the input values\r\n";
                }
            }
            else
            {
                textBoxInfo.Text = "Add the object to list.\r\n";
            }
        }

        string enter_item(string type_object, string name, string weight, string material)
        {
            string result_string = "Object = " + type_object + "    Name = " + name + "   Weight = " + weight + "   Material = " + material;
            return result_string;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((listBox2.Items.Count <= comboBox2.Items.Count) && !(FileNames.Contains(comboBox2.SelectedItem)))
            {
                listBox2.Items.Add(comboBox2.SelectedItem);
                FileNames.Add(Convert.ToString(comboBox2.SelectedItem));
                textBoxInfo2.Text = "Plugin adds in the list.";
            }
            else
                textBoxInfo2.Text = "Plugin doesn't add in the list.";
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedIndex != -1)
                {
                    string nameObject = Convert.ToString(listBox2.SelectedItem);
                    nameObject = nameObject.Substring(0, nameObject.Length - 4);
                    if (comboBox1.Items.Contains(nameObject))
                    {
                        comboBox1.Items.Remove(nameObject);
                    }
                    FileNames.Remove(Convert.ToString(listBox2.SelectedItem));
                    ListLoadPlugins.Remove(Convert.ToString(listBox2.SelectedItem));
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);                   
                    textBoxInfo2.Text = "Plugin removes in the list, comboBox.";
                }
                else
                    textBoxInfo2.Text = "Plugin doesn't remove in the list.";
            }
            catch
            {
                textBoxInfo2.Text = "Choose the object.\r\n";
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxInfo.Text = LoadPlugins(FileNames);
        }
        string LoadPlugins(List<string> FileNames)
        {

            ICollection<IPlugin> plugins = XML.serializaton.PluginLoader.LoadPlugins(Constants.PluginsPath, FileNames, ListLoadPlugins);
            if (plugins != null)
            {
                foreach (IPlugin plugin in plugins)
                {
                    plugin.Run(this);
                }
                return "The plugins were loaded successful!";

            }
            else
            {
                return "The plugins don't include.";
            }
        }

    }
}
