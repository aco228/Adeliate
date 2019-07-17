using Adlite.IPInserter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adlite.IPInserter
{
  public partial class Form1 : Form
  {

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Program.MobileOperatorTable = Program.AdliteDatabase.GetDataTableTemplate("IPCountryMobileOperatorMap");
      Program.CountryTable = Program.AdliteDatabase.GetDataTableTemplate("IPCountryMap");
      Program.Countries = CountryModel.LoadAll();
      btnInsert.Enabled = false;
    }

    private void btnFindFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.InitialDirectory = "c:\\";
      openFileDialog1.Filter = "csv files (*.csv)|*.csv";
      openFileDialog1.FilterIndex = 2;
      openFileDialog1.RestoreDirectory = true;

      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        labelFilePath.Text = openFileDialog1.FileName;
        btnInsert.Enabled = true;
        btnFindFile.Enabled = false;
        Program.FILE_COUNTRY_LOCATION = labelFilePath.Text;
      }
    }
    
    private void btnMnoFile_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.InitialDirectory = "c:\\";
      openFileDialog1.Filter = "csv files (*.csv)|*.csv";
      openFileDialog1.FilterIndex = 2;
      openFileDialog1.RestoreDirectory = true;

      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        labelFilePath.Text = openFileDialog1.FileName;
        btnInsert.Enabled = true;
        btnFindFile.Enabled = false;
        Program.FILE_MNO_LOCATION = labelFilePath.Text;
      }
    }

    private void btnInsert_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(Program.FILE_COUNTRY_LOCATION))
      {
        MessageBox.Show("Please enter country map file location");
        return;
      }

      if (string.IsNullOrEmpty(Program.FILE_MNO_LOCATION))
      {
        MessageBox.Show("Please enter mobile operator map file location");
        return;
      }

      labelMessage.Text = string.Format("Started");
      Program.AdliteDatabase.Execute("TRUNCATE TABLE Adlite.core.IPCountryMap;");
      Program.AdliteDatabase.Execute("TRUNCATE TABLE Adlite.core.IPCountryMobileOperatorMap;");

      this.InsertCountryMap();
      this.InsertMobileOperatorMap();
      
      MessageBox.Show("Finished");
    }

    public void InsertMobileOperatorMap()
    {
      Program.CURRENT_LIMIT = 0;
      Program.INSERTED_LIMIT = 0;

      const Int32 BufferSize = 128;
      using (var fileStream = File.OpenRead(Program.FILE_MNO_LOCATION))
      using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
      {
        String line;
        while ((line = streamReader.ReadLine()) != null)
        {
          Application.DoEvents();
          new LineModel(line);
          if (Program.CURRENT_LIMIT >= Program.QUERY_LIMIT)
          {
            Program.INSERTED_LIMIT += (ulong)Program.CURRENT_LIMIT;
            Program.CURRENT_LIMIT = 0;
            Program.AdliteDatabase.BulkCopy(Program.MobileOperatorTable, "IPCountryMobileOperatorMap");
            Program.MobileOperatorTable.DataTable.Clear();

            labelMessage.Text = string.Format("Inserted {0}", Program.INSERTED_LIMIT);
            Application.DoEvents();
          }
        }
      }

      Program.AdliteDatabase.BulkCopy(Program.MobileOperatorTable, "IPCountryMobileOperatorMap");
      Program.MobileOperatorTable.DataTable.Clear();
      labelMessage.Text = string.Format("Inserted {0}", Program.INSERTED_LIMIT);
      Application.DoEvents();

      MessageBox.Show("MobileOpeator map inserted");
    }

    public void InsertCountryMap()
    {
      Program.CURRENT_LIMIT = 0;
      Program.INSERTED_LIMIT = 0;

      const Int32 BufferSize = 128;
      using (var fileStream = File.OpenRead(Program.FILE_COUNTRY_LOCATION))
      using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
      {
        String line;
        while ((line = streamReader.ReadLine()) != null)
        {
          Application.DoEvents();
          LineModel.CreateCountryMap(line);
          if (Program.CURRENT_LIMIT >= Program.QUERY_LIMIT)
          {
            Program.INSERTED_LIMIT += (ulong)Program.CURRENT_LIMIT;
            Program.CURRENT_LIMIT = 0;
            Program.AdliteDatabase.BulkCopy(Program.CountryTable, "IPCountryMap");
            Program.CountryTable.DataTable.Clear();

            labelMessage.Text = string.Format("Inserted {0}", Program.INSERTED_LIMIT);
            Application.DoEvents();
          }
        }
      }

      Program.AdliteDatabase.BulkCopy(Program.CountryTable, "IPCountryMap");
      Program.CountryTable.DataTable.Clear();
      labelMessage.Text = string.Format("Inserted {0}", Program.INSERTED_LIMIT);
      Application.DoEvents();

      MessageBox.Show("Country map inserted");
    }

  }
}
