using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirSyncroClient.SyncService;
using DirSyncro;
using DirSyncroClient.Properties;

namespace DirSyncroClient
{
    public partial class DirSyncroForm : Form
    {
        private static SyncServiceClient ssc;
        private static readonly string configurationFile = @"..\..\..\DirSyncro\bin\Debug\DirSyncro.xml";
        private DirSyncro.DirSyncro config;

        public DirSyncroForm()
        {
            InitializeComponent();
            ssc = new SyncServiceClient();

            config = Utility.ReadFromXML<DirSyncro.DirSyncro>(configurationFile);

            watcherGrid.Rows.Add(config.Watcher.Length);
            for (int i = 0; i < config.Watcher.Length; i++)
            {
                watcherGrid.Rows[i].Cells["enabled"].Value = new DataGridViewImageCell();
                watcherGrid.Rows[i].Cells["watcher"].Value = new DataGridViewTextBoxCell();
            }
            for (int i = 0; i < config.Watcher.Length; i++)
            {
                if (config.Watcher[i].Enabled)
                {
                    watcherGrid.Rows[i].Cells["enabled"].Tag = true;
                    watcherGrid.Rows[i].Cells["enabled"].Value = Resources.On;
                }
                else
                {
                    watcherGrid.Rows[i].Cells["enabled"].Tag = false;
                    watcherGrid.Rows[i].Cells["enabled"].Value = Resources.Off;
                }
                watcherGrid.Rows[i].Cells["watcher"].Value = config.Watcher[i].Name;
                watcherGrid.Rows[i].Tag = i;
            }
        }

        private void watcherGrid_MouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                int index = (int)dgv.Rows[e.RowIndex].Tag;
                if (e.ColumnIndex == 0)
                {
                    DataGridViewImageCell enabled = (DataGridViewImageCell)watcherGrid[e.ColumnIndex, e.RowIndex];
                    if ((bool)enabled.Tag == true)
                    {
                        if (ssc.ServiceCommand(1, config.Watcher[index].Name))
                        {
                            enabled.Value = Resources.Off;
                            enabled.Tag = false;
                            config.Watcher[index].Enabled = false;
                        }
                        else
                        {
                            // todo - display error
                        }
                    }
                    else
                    {
                        if (ssc.ServiceCommand(0, config.Watcher[index].Name))
                        {
                            enabled.Value = Resources.On;
                            enabled.Tag = true;
                            config.Watcher[index].Enabled = true;
                        }
                        else
                        {
                            // todo - display error
                        }
                    }
                }
            }
        }
    }
}
