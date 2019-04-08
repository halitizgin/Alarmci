using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.IO;
using WMPLib;
using System.Collections;

namespace Alarm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=alarmci.accdb");
        OleDbCommand komut = new OleDbCommand();

        string gBaslik = "";
        string gMetin = "";
        string gIcon = "";
        string cesit = "";
        string gKonum = "";
        DateTime gSaat;
        SoundPlayer oynat = new SoundPlayer();
        public bool alarm = false;
        int gSure = 0;
        int sure = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (alarm == false)
                {
                    if (radioButton1.Checked == true)
                    {
                        if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "")
                        {
                            cesit = "mesaj";
                            gBaslik = textBox1.Text;
                            gMetin = textBox2.Text;
                            gIcon = comboBox1.Text;
                            gSaat = Convert.ToDateTime(maskedTextBox1.Value);
                            alarm = true;
                            string format = "T";
                            timer1.Enabled = true;
                            button1.Text = "Alarmı Durdur";
                            label6.Text = "Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            this.Text = "Alarmcı v5.8 | Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            MessageBox.Show("Alarm " + gSaat.ToString(format) + " zamanına kuruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioButton2.Checked == true)
                    {
                        if (maskedTextBox1.Text != "" && textBox3.Text != "")
                        {
                            cesit = "ses";
                            gKonum = textBox3.Text;
                            gSaat = Convert.ToDateTime(maskedTextBox1.Value);
                            alarm = true;
                            string format = "T";
                            timer1.Enabled = true;
                            button1.Text = "Alarmı Durdur";
                            label6.Text = "Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            this.Text = "Alarmcı v5.8 | Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            MessageBox.Show("Alarm " + gSaat.ToString(format) + " zamanına kuruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioButton3.Checked == true)
                    {
                        if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" || comboBox1.Text == "Hata" || comboBox1.Text == "Bilgi" || comboBox1.Text == "Hiç")
                        {
                            sure = int.Parse(textBox4.Text);
                            if (sure > 0 && sure <= 20)
                            {
                                cesit = "tray";
                                gSure = sure;
                                gBaslik = textBox1.Text;
                                gMetin = textBox2.Text;
                                gIcon = comboBox1.Text;
                                gSaat = Convert.ToDateTime(maskedTextBox1.Value);
                                alarm = true;
                                string format = "T";
                                timer1.Enabled = true;
                                button1.Text = "Alarmı Durdur";
                                label6.Text = "Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                                this.Text = "Alarmcı v5.8 | Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                                MessageBox.Show("Alarm " + gSaat.ToString(format) + " zamanına kuruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Süre en az 1 sn, en fazla 20 sn olarak ayarlanabilir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (radioButton4.Checked == true)
                    {
                        if (comboBox2.Text.Trim() != "")
                        {
                            cesit = "ses";
                            gKonum = Application.StartupPath.Replace("bin\\Debug", "Resources") + "\\" + comboBox2.Text.Trim();
                            gSaat = Convert.ToDateTime(maskedTextBox1.Value);
                            alarm = true;
                            string format = "T";
                            timer1.Enabled = true;
                            button1.Text = "Alarmı Durdur";
                            label6.Text = "Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            this.Text = "Alarmcı v5.8 | Durum: Alarm Açık (" + gSaat.ToString(format) + ")";
                            MessageBox.Show("Alarm " + gSaat.ToString(format) + " zamanına kuruldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (alarm == true)
                {
                    gBaslik = "";
                    gMetin = "";
                    gIcon = "";
                    gKonum = "";
                    cesit = "";
                    oynat.Stop();
                    alarm = false;
                    button1.Text = "Alarm Kur";
                    label6.Text = "Durum: Alarm Kapalı";
                    this.Text = "Alarmcı v5.8 | Durum: Alarm Kapalı";
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cesit == "mesaj")
                {
                    if (gBaslik != "" && gMetin != "" && gIcon != "")
                    {
                        DateTime zaman = DateTime.Now;
                        string format = "T";
                        if (zaman.ToString(format) == gSaat.ToString(format))
                        {
                            if (gIcon == "Hata")
                            {
                                alarm = false;
                                timer1.Enabled = false;
                                MessageBox.Show(gMetin, gBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (gIcon == "Bilgi")
                            {
                                alarm = false;
                                timer1.Enabled = false;
                                MessageBox.Show(gMetin, gBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (gIcon == "Soru")
                            {
                                alarm = false;
                                timer1.Enabled = false;
                                MessageBox.Show(gMetin, gBaslik, MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                            alarm = false;
                            label6.Text = "Durum: Alarm Kapalı";
                            this.Text = "Alarmcı v5.8 | Durum: Alarm Kapalı";
                            button1.Text = "Alarm Kur";
                        }
                    }
                }
                else if (cesit == "ses")
                {
                    if (gKonum != "")
                    {
                        DateTime zaman = DateTime.Now;
                        string format = "T";
                        if (zaman.ToString(format) == gSaat.ToString(format))
                        {
                            wplayer.URL = gKonum;
                            wplayer.controls.play();
                            frmAlarm frmAlarm = new frmAlarm();
                            frmAlarm.Show();
                        }
                    }
                }
                else if (cesit == "tray")
                {
                    if (gBaslik != "" && gMetin != "" && gIcon != "")
                    {
                        DateTime zaman = DateTime.Now;
                        string format = "T";
                        if (zaman.ToString(format) == gSaat.ToString(format))
                        {
                            if (gIcon == "Hata")
                            {
                                notifyIcon1.BalloonTipText = gMetin;
                                notifyIcon1.BalloonTipTitle = gBaslik;
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                                notifyIcon1.ShowBalloonTip(gSure);
                            }
                            else if (gIcon == "Bilgi")
                            {
                                notifyIcon1.BalloonTipText = gMetin;
                                notifyIcon1.BalloonTipTitle = gBaslik;
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                notifyIcon1.ShowBalloonTip(gSure);
                            }
                            else if (gIcon == "Soru")
                            {
                                notifyIcon1.BalloonTipText = gMetin;
                                notifyIcon1.BalloonTipTitle = gBaslik;
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
                                notifyIcon1.ShowBalloonTip(gSure);
                            }
                            alarm = false;
                            label6.Text = "Durum: Alarm Kapalı";
                            this.Text = "Alarmcı v5.8 | Durum: Alarm Kapalı";
                            button1.Text = "Alarm Kur";
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime zaman = DateTime.Now;
                string format = "T";
                maskedTextBox2.Text = zaman.ToString(format);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                maskedTextBox1.CustomFormat = "MMMM dd, yyyy - dddd - HH:mm:ss";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ArrayList quickList(params string[] parameters)
        {
            ArrayList veriler = new ArrayList();
            foreach (string item in parameters)
            {
                veriler.Add(item);
            }
            return veriler;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList uzantilar = quickList(".ogg", ".aac", ".flac", ".mp3", ".mp2", ".wma", ".m4a", ".wav", ".wmv", ".mp4", ".mkv", ".mov", ".3gp", ".flv", ".mpg", ".webm");
                openFileDialog1.Filter = "Müzik Dosyaları (.ogg, .aac, .flac, .mp3, .mp2, .wma, .m4a, .wav) |*.ogg; *.aac; *.flac; *.mp3; *.mp2; *.wma; *.m4a; *.wav|Video Dosyaları (.wmv, .mp4, .mkv, .mov, .3gp, .flv, .mpg, .webm) |*.wmv; *.mp4; *.mkv; *.mov; *.3gp; *.flv; *.mpg; *.webm|Tüm Dosyalar |*.*";
                openFileDialog1.Title = "Dosya Seçiniz";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string uzanti = System.IO.Path.GetExtension(openFileDialog1.FileName);
                    if (uzantilar.Contains(uzanti))
                    {
                        textBox3.Text = openFileDialog1.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Bu dosya desteklenmiyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    comboBox1.Enabled = true;
                    label7.Enabled = false;
                    textBox3.Enabled = false;
                    button2.Enabled = false;
                    textBox4.Enabled = false;
                    label8.Enabled = false;
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Hata");
                    comboBox1.Items.Add("Bilgi");
                    comboBox1.Items.Add("Soru");
                    comboBox2.Enabled = false;
                    label9.Enabled = false;
                    button6.Enabled = false;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked == true)
                {
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    label7.Enabled = true;
                    textBox3.Enabled = true;
                    button2.Enabled = true;
                    textBox4.Enabled = false;
                    label8.Enabled = false;
                    comboBox2.Enabled = false;
                    label9.Enabled = false;
                    button6.Enabled = false;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool hata = false;
                if (radioButton1.Checked == true)
                {
                    if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && comboBox1.Text.Trim() != "" && comboBox1.Text == "Bilgi" || comboBox1.Text == "Soru" || comboBox1.Text == "Hata")
                    {
                        string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Alarm Adı: ", "Alarm Kaydet", "", 50, 50);
                        if (nAlarmadi.Trim() != "")
                        {
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "SELECT * FROM alarm";
                            OleDbDataReader okuyucu = komut.ExecuteReader();
                            while (okuyucu.Read())
                            {
                                if (nAlarmadi == okuyucu["alarmadi"].ToString())
                                {
                                    hata = true;
                                }
                            }
                            komut.Dispose();
                            baglanti.Close();

                            if (hata == true)
                            {
                                MessageBox.Show("Belirttiğiniz alarm adında zaten bir alarm kaydı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string nBaslik = textBox1.Text.Trim();
                                string nMetin = textBox2.Text.Trim();
                                string nIcon = comboBox1.Text.Trim();
                                DateTime nTarih = maskedTextBox1.Value;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "INSERT INTO alarm (alarmadi, alarmzamani, Baslik, Metin, Icon, Cesit) VALUES ('" + nAlarmadi + "', '" + nTarih + "', '" + nBaslik + "', '" + nMetin + "', '" + nIcon + "', 'Mesaj')";
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                MessageBox.Show("Alarm kayıt edildi.\nAlarm Adı: " + nAlarmadi + "\nAlarm Zamanı: " + nTarih.ToString("MMMM dd, yyyy  dddd - HH:mm:ss") + "\nBaşlık: " + nBaslik + "\nMetin: " + nMetin + "\nIcon: " + nIcon, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gerekli yerleri doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    if (textBox3.Text.Trim() != "")
                    {
                        string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Alarm Adı: ", "Alarm Kaydet", "", 50, 50);
                        if (nAlarmadi.Trim() != "")
                        {
                            string nSes = textBox3.Text.Trim();
                            DateTime nTarih = maskedTextBox1.Value;
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "INSERT INTO alarm (alarmadi, alarmzamani, Ses, Cesit) VALUES ('" + nAlarmadi + "', '" + nTarih + "', '" + nSes + "', 'Ses')";
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                            MessageBox.Show("Alarm kayıt edildi.\nAlarm Adı: " + nAlarmadi + "\nAlarm Zamanı: " + nTarih.ToString("MMMM dd, yyyy  dddd - HH:mm:ss") + "\nÇalacak Müzik Konumu: " + nSes, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox4.Text.Trim() != "" && comboBox1.Text.Trim() == "Bilgi" || comboBox1.Text.Trim() == "Hata" || comboBox1.Text.Trim() == "Uyarı" || comboBox1.Text.Trim() == "Hiç")
                    {
                        string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Alarm Adı: ", "Alarm Kaydet", "", 50, 50);
                        if (nAlarmadi.Trim() != "")
                        {
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "SELECT * FROM alarm";
                            OleDbDataReader okuyucu = komut.ExecuteReader();
                            while (okuyucu.Read())
                            {
                                if (nAlarmadi == okuyucu["alarmadi"].ToString())
                                {
                                    hata = true;
                                }
                            }
                            komut.Dispose();
                            baglanti.Close();

                            if (hata == true)
                            {
                                MessageBox.Show("Belirttiğiniz alarm adında zaten bir alarm kaydı mevcuttur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string nBaslik = textBox1.Text.Trim();
                                string nMetin = textBox2.Text.Trim();
                                string nIcon = comboBox1.Text.Trim();
                                string nSure = textBox4.Text.Trim();
                                DateTime nTarih = maskedTextBox1.Value;
                                baglanti.Open();
                                komut.Connection = baglanti;
                                komut.CommandText = "INSERT INTO alarm (alarmadi, alarmzamani, Baslik, Metin, Icon, Cesit, Sure) VALUES ('" + nAlarmadi + "', '" + nTarih + "', '" + nBaslik + "', '" + nMetin + "', '" + nIcon + "', 'Tray', @sure)";
                                komut.Parameters.AddWithValue("@sure", nSure);
                                komut.ExecuteNonQuery();
                                komut.Dispose();
                                baglanti.Close();
                                MessageBox.Show("Alarm kayıt edildi.\nAlarm Adı: " + nAlarmadi + "\nAlarm Zamanı: " + nTarih.ToString("MMMM dd, yyyy  dddd - HH:mm:ss") + "\nBaşlık: " + nBaslik + "\nMetin: " + nMetin + "\nIcon: " + nIcon, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else if (radioButton4.Checked == true)
                {
                    if (comboBox2.Text.Trim() != "")
                    {
                        string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Alarm Adı: ", "Alarm Kaydet", "", 50, 50);
                        if (nAlarmadi.Trim() != "")
                        {
                            string nSes = comboBox2.Text.Trim();
                            DateTime nTarih = maskedTextBox1.Value;
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "INSERT INTO alarm (alarmadi, alarmzamani, Ses, Cesit) VALUES ('" + nAlarmadi + "', '" + nTarih + "', '" + nSes + "', 'Hazır Ses')";
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                            MessageBox.Show("Alarm kayıt edildi.\nAlarm Adı: " + nAlarmadi + "\nAlarm Zamanı: " + nTarih.ToString("MMMM dd, yyyy  dddd - HH:mm:ss") + "\nÇalacak Müzik Konumu: " + nSes, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool hata = true;
                string nBaslik = "";
                string nMetin = "";
                string nIcon = "";
                string nSes = "";
                string nCesit = "";
                int nSure = 0;
                DateTime nTarih = DateTime.Now;
                string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Açılacak kayıtlı alarmın adını giriniz: ", "Alarm Aç", "", 50, 50);
                if (nAlarmadi.Trim() != "")
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "SELECT * FROM alarm";
                    OleDbDataReader okuyucu = komut.ExecuteReader();
                    while (okuyucu.Read())
                    {
                        if (nAlarmadi == okuyucu["alarmadi"].ToString())
                        {
                            hata = false;
                            nBaslik = okuyucu["Baslik"].ToString();
                            nMetin = okuyucu["Metin"].ToString();
                            nIcon = okuyucu["Icon"].ToString(); ;
                            nTarih = DateTime.Parse(okuyucu["alarmzamani"].ToString());
                            nSes = okuyucu["Ses"].ToString();
                            nCesit = okuyucu["Cesit"].ToString();
                            nSure = int.Parse(okuyucu["Sure"].ToString());
                        }
                    }
                    komut.Dispose();
                    baglanti.Close();
                    if (hata == true)
                    {
                        MessageBox.Show("Belirttiğiniz isimde herhangi bir alarm kaydı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (nCesit == "Mesaj")
                        {
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                            radioButton3.Checked = false;
                            radioButton4.Checked = false;
                            textBox1.Text = nBaslik;
                            textBox2.Text = nMetin;
                            comboBox1.Text = nIcon;
                            maskedTextBox1.Value = nTarih;
                        }
                        else if (nCesit == "Ses")
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = true;
                            radioButton3.Checked = false;
                            radioButton4.Checked = false;
                            textBox3.Text = nSes;
                            maskedTextBox1.Value = nTarih;
                        }
                        else if (nCesit == "Tray")
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            radioButton3.Checked = true;
                            radioButton4.Checked = false;
                            textBox1.Text = nBaslik;
                            textBox2.Text = nMetin;
                            comboBox1.Text = nIcon;
                            maskedTextBox1.Value = nTarih;
                            textBox4.Text = nSure.ToString();
                        }
                        else if (nCesit == "Hazır Ses")
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = false;
                            radioButton3.Checked = false;
                            radioButton4.Checked = true;
                            maskedTextBox1.Value = nTarih;
                            comboBox2.Text = Path.GetFileName(nSes);
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string nAlarmadi = Microsoft.VisualBasic.Interaction.InputBox("Silinecek kayıtlı alarmın adını giriniz: ", "Alarm Aç", "", 50, 50);
                if (nAlarmadi.Trim() != "")
                {
                    bool hata = true;
                    int nID = 0;
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "SELECT * FROM alarm";
                    OleDbDataReader okuyucu = komut.ExecuteReader();
                    while (okuyucu.Read())
                    {
                        if (nAlarmadi == okuyucu["alarmadi"].ToString())
                        {
                            hata = false;
                            nID = int.Parse(okuyucu["id"].ToString());
                        }
                    }
                    komut.Dispose();
                    baglanti.Close();

                    if (hata == true)
                    {
                        MessageBox.Show("Belirttiğiniz isimde herhangi bir alarm kaydı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult soru = MessageBox.Show("\"" + nAlarmadi.Trim() + "\" adlı kaydı silmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (soru == DialogResult.Yes)
                        {
                            baglanti.Open();
                            komut.Connection = baglanti;
                            komut.CommandText = "DELETE FROM alarm WHERE id=@id";
                            komut.Parameters.AddWithValue("@id", nID);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                            MessageBox.Show("\"" + nAlarmadi.Trim() + "\" adlı alarm kayıt başarıyla silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    notifyIcon1.BalloonTipTitle = this.Text;
                    notifyIcon1.BalloonTipText = this.Text;
                    this.Hide();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void alarmıDurdurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Show();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void alarmıDurdurToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (alarm == true)
                {
                    gBaslik = "";
                    gMetin = "";
                    gIcon = "";
                    gKonum = "";
                    cesit = "";
                    oynat.Stop();
                    alarm = false;
                    button1.Text = "Alarm Kur";
                    label6.Text = "Durum: Alarm Kapalı";
                    this.Text = "Alarmcı v5.8 | Durum: Alarm Kapalı";
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.BalloonTipText = "Whatever";
                notifyIcon1.BalloonTipTitle = "Alarm";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(3);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton3.Checked == true)
                {
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    comboBox1.Enabled = true;
                    label7.Enabled = false;
                    textBox3.Enabled = false;
                    button2.Enabled = false;
                    textBox4.Enabled = true;
                    label8.Enabled = true;
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("Hata");
                    comboBox1.Items.Add("Bilgi");
                    comboBox1.Items.Add("Hiç");
                    comboBox2.Enabled = false;
                    label9.Enabled = false;
                    button6.Enabled = false;

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton4.Checked == true)
                {
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    label7.Enabled = false;
                    textBox3.Enabled = false;
                    button2.Enabled = false;
                    textBox4.Enabled = false;
                    label8.Enabled = false;
                    comboBox2.Enabled = true;
                    label9.Enabled = true;
                    button6.Enabled = true;
                    button8.Enabled = false;
                    string[] fileEntries = Directory.GetFiles(Application.StartupPath.Replace("bin\\Debug", "Resources"));
                    foreach (string fileName in fileEntries)
                    {
                        ArrayList uzantilar = quickList(".ogg", ".aac", ".flac", ".mp3", ".mp2", ".wma", ".m4a", ".wav", ".wmv", ".mp4", ".mkv", ".mov", ".3gp", ".flv", ".mpg", ".webm");
                        string uzanti = Path.GetExtension(fileName);
                        if (uzantilar.Contains(uzanti))
                        {
                            comboBox2.Items.Add(Path.GetFileName(fileName));
                        }
                    }

                    string olmayanlar = "";
                    ArrayList hazirsesler = quickList("Ses10.wav", "Ses11.wav", "Ses12.wav", "Ses13.wav", "Ses13.wav", "Ses13.wav", "Ses14.wav", "Ses15.wav", "Ses16.wav", "Ses17.wav", "Ses18.wav", "Ses19.wav", "Ses20.wav", "Ses21.wav", "Ses22.wav", "Ses23.wav", "Ses24.wav", "Ses25.wav", "Ses26.wav", "Ses27.wav", "Ses28.wav", "Ses29.wav", "Ses30.wav", "Ses31.wav");
                    foreach (string item in hazirsesler)
                    {
                        if (!comboBox2.Items.Contains(item))
                        {
                            olmayanlar += item + "\n";
                        }
                    }

                    if (olmayanlar != "")
                    {
                        MessageBox.Show("Aşağıdaki hazır ses dosyaları eksik:\n\n" + olmayanlar, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text.Trim() != "")
                {
                    Alarm.Properties.Resources.ResourceManager.GetObject(comboBox2.Text.Trim());
                    oynat.SoundLocation = Application.StartupPath.Replace("bin\\Debug", "Resources") + "\\" + comboBox2.Text.Trim();
                    oynat.PlayLooping();
                    button8.Enabled = true;
                }
                
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            oynat.Stop();
            button8.Enabled = false;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string nMetin = "";
            string nBaslik = "";
            int nSure = 0;
            string nIcon = "";
            MessageBoxIcon icon = MessageBoxIcon.None;

            if (radioButton1.Checked == true)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "")
                {
                    nBaslik = textBox1.Text;
                    nMetin = textBox2.Text;
                    nIcon = comboBox1.Text;
                    if (nIcon == "Hata")
                    {
                        icon = MessageBoxIcon.Error;
                    }
                    else if (nIcon == "Bilgi")
                    {
                        icon = MessageBoxIcon.Information;
                    }
                    else if (nIcon == "Soru")
                    {
                        icon = MessageBoxIcon.Question;
                    }
                    MessageBox.Show(nMetin, nBaslik, MessageBoxButtons.OK, icon);
                }
            }
            else if (radioButton3.Checked == true)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" || comboBox1.Text == "Hata" || comboBox1.Text == "Bilgi" || comboBox1.Text == "Hiç")
                {
                    sure = int.Parse(textBox4.Text);
                    if (sure > 0 && sure <= 20)
                    {
                        nSure = sure;
                        nBaslik = textBox1.Text;
                        nMetin = textBox2.Text;
                        nIcon = comboBox1.Text;
                        if (nIcon == "Hiç")
                        {
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.None;
                        }
                        else if (nIcon == "Bilgi")
                        {
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        }
                        else if (nIcon == "Uyarı")
                        {
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                        }
                        else if (nIcon == "Hata")
                        {
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                        }
                        notifyIcon1.BalloonTipText = nMetin;
                        notifyIcon1.BalloonTipTitle = nBaslik;
                        notifyIcon1.ShowBalloonTip(nSure);
                    }
                    else
                    {
                        MessageBox.Show("Süre en az 1 sn, en fazla 20 sn olarak ayarlanabilir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.Visible == true)
            {
                contextMenuStrip1.Items[1].Enabled = false;
                contextMenuStrip1.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip1.Items[1].Enabled = true;
                contextMenuStrip1.Items[2].Enabled = false;
            }

            if (alarm == true)
            {
                contextMenuStrip1.Items[0].Enabled = true;
            }
            else
            {
                contextMenuStrip1.Items[0].Enabled = false;
            }
        }

        private void yenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (radioButton4.Checked == true)
                {
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    label7.Enabled = false;
                    textBox3.Enabled = false;
                    button2.Enabled = false;
                    textBox4.Enabled = false;
                    label8.Enabled = false;
                    comboBox2.Enabled = true;
                    label9.Enabled = true;
                    button6.Enabled = true;
                    button8.Enabled = false;
                    comboBox2.Items.Clear();
                    string[] fileEntries = Directory.GetFiles(Application.StartupPath.Replace("bin\\Debug", "Resources"));
                    foreach (string fileName in fileEntries)
                    {
                        ArrayList uzantilar = quickList(".ogg", ".aac", ".flac", ".mp3", ".mp2", ".wma", ".m4a", ".wav", ".wmv", ".mp4", ".mkv", ".mov", ".3gp", ".flv", ".mpg", ".webm");
                        string uzanti = Path.GetExtension(fileName);
                        if (uzantilar.Contains(uzanti))
                        {
                            comboBox2.Items.Add(Path.GetFileName(fileName));
                        }
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
