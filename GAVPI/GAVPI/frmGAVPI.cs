﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    public partial class frmGAVPI : Form
    {
        string BUILD_VERSION = "GAVPI Alpha Build 0.03 08.10.14";

        VI_Settings vi_settings;
        VI_Profile vi_profile;
        VI vi;

        public frmGAVPI()
        {
            InitializeComponent();
        }
        #region Main form
        private void frmGAVPI_Load(object sender, EventArgs e)
        {
            vi = new VI();
            vi_settings = new VI_Settings();
            vi_profile = new VI_Profile(vi_settings.current_profile_path);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        #endregion
        #region Profile
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProfile modProfileFrm = new frmProfile(vi_profile);
                modProfileFrm.ShowDialog();
                modProfileFrm.Dispose();
            }
            catch (Exception profile_exception)
            {
                MessageBox.Show("Profile Editor Crashed.\n" + profile_exception.Message, "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }
            finally
            {
                
            }
        }
        #endregion
        #region Settings
        private void mainStripSettings_Click(object sender, EventArgs e)
        {
            frmSettings modSettingsFrm = new frmSettings(vi_settings);
            modSettingsFrm.ShowDialog();
        }
        #endregion

        private void btnMainListen_Click(object sender, EventArgs e)
        {
            vi.load_listen(vi_profile, vi_settings, lstMainHearing);
            btnMainListen.Enabled = false;
            btmStripStatus.Text = "active";
        }

        private void btnMainStop_Click(object sender, EventArgs e)
        {
            // Stop
            vi.stop_listen();
            // Clean
            vi = new VI();

            btnMainListen.Enabled = true;
            btmStripStatus.Text = "inactive";
        }

        private void mainStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(BUILD_VERSION);
        }
    }
}
