///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using vcFramework.Delegates;
using vcFramework.Interfaces;
using vcFramework.Threads;
using vcFramework.Windows.Forms;
using BrowseMonkeyData;
using FSXml;

namespace BrowseMonkey
{

	/// <summary> 
	/// 
	/// </summary>
    public class VolumeBrowser : Form, IDirty, ISpawned
	{

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tcVolume = new System.Windows.Forms.TabControl();
			this.tpExplorer = new System.Windows.Forms.TabPage();
			this.gbExplorer = new System.Windows.Forms.GroupBox();
			this.fsXmlExplorer = new FSXmlWinUI.FSXmlExplorer();
			this.tpFileList = new System.Windows.Forms.TabPage();
			this.gbFileList = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel9 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnDumpFileList = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.lvAllFiles = new vcFramework.Windows.Forms.ListViewSP();
			this.pnlFileListStatus = new System.Windows.Forms.Panel();
			this.sbFileList = new System.Windows.Forms.StatusBar();
			this.sbpFilesListed = new System.Windows.Forms.StatusBarPanel();
			this.lblWaiting = new System.Windows.Forms.Label();
			this.pnlFileListProgress = new System.Windows.Forms.Panel();
			this.lblFileList = new System.Windows.Forms.Label();
			this.pbFileList = new System.Windows.Forms.ProgressBar();
			this.btnCancelFileList = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlFileListLeft = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel10 = new System.Windows.Forms.Panel();
			this.panel12 = new System.Windows.Forms.Panel();
			this.btnListFiles = new System.Windows.Forms.Button();
			this.panel8 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCheckAll = new System.Windows.Forms.Button();
			this.btnUncheckAll = new System.Windows.Forms.Button();
			this.panel6 = new System.Windows.Forms.Panel();
			this.clbFileTypes = new System.Windows.Forms.CheckedListBox();
			this.tpProperties = new System.Windows.Forms.TabPage();
			this.groupBoxXP2 = new System.Windows.Forms.GroupBox();
			this.usrcVolumeProperties = new BrowseMonkey.VolumeProperties();
			this.panel3 = new System.Windows.Forms.Panel();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.pbVolumeLoad = new System.Windows.Forms.ProgressBar();
			this.lblLoadProgressText = new System.Windows.Forms.Label();
			this.panelGallery1 = new vcFramework.Windows.Forms.PanelGallery();
			this.pnlLoadInfo = new System.Windows.Forms.Panel();
			this.pnlContent = new System.Windows.Forms.Panel();
			this.tcVolume.SuspendLayout();
			this.tpExplorer.SuspendLayout();
			this.gbExplorer.SuspendLayout();
			this.tpFileList.SuspendLayout();
			this.gbFileList.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel9.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.pnlFileListStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesListed)).BeginInit();
			this.pnlFileListProgress.SuspendLayout();
			this.pnlFileListLeft.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel10.SuspendLayout();
			this.panel12.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel6.SuspendLayout();
			this.tpProperties.SuspendLayout();
			this.groupBoxXP2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panelGallery1.SuspendLayout();
			this.pnlLoadInfo.SuspendLayout();
			this.pnlContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// tcVolume
			// 
			this.tcVolume.Controls.Add(this.tpExplorer);
			this.tcVolume.Controls.Add(this.tpFileList);
			this.tcVolume.Controls.Add(this.tpProperties);
			this.tcVolume.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcVolume.Location = new System.Drawing.Point(0, 0);
			this.tcVolume.Multiline = true;
			this.tcVolume.Name = "tcVolume";
			this.tcVolume.SelectedIndex = 0;
			this.tcVolume.Size = new System.Drawing.Size(664, 558);
			this.tcVolume.TabIndex = 1;
			// 
			// tpExplorer
			// 
			this.tpExplorer.Controls.Add(this.gbExplorer);
			this.tpExplorer.Location = new System.Drawing.Point(4, 22);
			this.tpExplorer.Name = "tpExplorer";
			this.tpExplorer.Size = new System.Drawing.Size(656, 532);
			this.tpExplorer.TabIndex = 8;
			this.tpExplorer.Text = "Explorer";
			// 
			// gbExplorer
			// 
			this.gbExplorer.Controls.Add(this.fsXmlExplorer);
			this.gbExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbExplorer.Location = new System.Drawing.Point(0, 0);
			this.gbExplorer.Name = "gbExplorer";
			this.gbExplorer.Size = new System.Drawing.Size(656, 532);
			this.gbExplorer.TabIndex = 0;
			this.gbExplorer.TabStop = false;
			// 
			// fsXmlExplorer
			// 
			this.fsXmlExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fsXmlExplorer.Location = new System.Drawing.Point(3, 16);
			this.fsXmlExplorer.Name = "fsXmlExplorer";
			this.fsXmlExplorer.Size = new System.Drawing.Size(650, 513);
			this.fsXmlExplorer.TabIndex = 0;
			// 
			// tpFileList
			// 
			this.tpFileList.Controls.Add(this.gbFileList);
			this.tpFileList.Location = new System.Drawing.Point(4, 22);
			this.tpFileList.Name = "tpFileList";
			this.tpFileList.Size = new System.Drawing.Size(656, 532);
			this.tpFileList.TabIndex = 7;
			this.tpFileList.Text = "File List";
			// 
			// gbFileList
			// 
			this.gbFileList.Controls.Add(this.panel2);
			this.gbFileList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbFileList.Location = new System.Drawing.Point(0, 0);
			this.gbFileList.Name = "gbFileList";
			this.gbFileList.Size = new System.Drawing.Size(656, 532);
			this.gbFileList.TabIndex = 0;
			this.gbFileList.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel9);
			this.panel2.Controls.Add(this.splitter1);
			this.panel2.Controls.Add(this.pnlFileListLeft);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(650, 513);
			this.panel2.TabIndex = 13;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.panel4);
			this.panel9.Controls.Add(this.panel1);
			this.panel9.Controls.Add(this.panel5);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel9.Location = new System.Drawing.Point(123, 0);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(527, 513);
			this.panel9.TabIndex = 16;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.btnDumpFileList);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(0, 483);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(527, 30);
			this.panel4.TabIndex = 11;
			// 
			// btnDumpFileList
			// 
			this.btnDumpFileList.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnDumpFileList.Location = new System.Drawing.Point(212, 4);
			this.btnDumpFileList.Name = "btnDumpFileList";
			this.btnDumpFileList.Size = new System.Drawing.Size(128, 23);
			this.btnDumpFileList.TabIndex = 12;
			this.btnDumpFileList.Text = "Get list in plain text ->";
			this.btnDumpFileList.Click += new System.EventHandler(this.btnTextDump_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(527, 24);
			this.panel1.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(360, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Use the file lister to generate filtered lists of files in this volume.";
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.lvAllFiles);
			this.panel5.Controls.Add(this.pnlFileListStatus);
			this.panel5.Location = new System.Drawing.Point(0, 24);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(524, 458);
			this.panel5.TabIndex = 8;
			// 
			// lvAllFiles
			// 
			this.lvAllFiles.AllowKeyboardDeleteKeyDeletion = false;
			this.lvAllFiles.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lvAllFiles.BackColor = System.Drawing.SystemColors.Window;
			this.lvAllFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvAllFiles.FocusNewItemOnInsert = false;
			this.lvAllFiles.FullRowSelect = true;
			this.lvAllFiles.HideSelection = false;
			this.lvAllFiles.InsertPosition = vcFramework.Windows.Forms.ListViewSP.InsertPositions.Top;
			this.lvAllFiles.Location = new System.Drawing.Point(218, 201);
			this.lvAllFiles.LockWidthOnZeroWidthColumns = false;
			this.lvAllFiles.Name = "lvAllFiles";
			this.lvAllFiles.Size = new System.Drawing.Size(72, 32);
			this.lvAllFiles.TabIndex = 5;
			this.lvAllFiles.View = System.Windows.Forms.View.Details;
			// 
			// pnlFileListStatus
			// 
			this.pnlFileListStatus.Controls.Add(this.sbFileList);
			this.pnlFileListStatus.Controls.Add(this.lblWaiting);
			this.pnlFileListStatus.Controls.Add(this.pnlFileListProgress);
			this.pnlFileListStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlFileListStatus.Location = new System.Drawing.Point(0, 432);
			this.pnlFileListStatus.Name = "pnlFileListStatus";
			this.pnlFileListStatus.Size = new System.Drawing.Size(522, 24);
			this.pnlFileListStatus.TabIndex = 9;
			// 
			// sbFileList
			// 
			this.sbFileList.Dock = System.Windows.Forms.DockStyle.None;
			this.sbFileList.Location = new System.Drawing.Point(344, 0);
			this.sbFileList.Name = "sbFileList";
			this.sbFileList.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.sbpFilesListed});
			this.sbFileList.ShowPanels = true;
			this.sbFileList.Size = new System.Drawing.Size(56, 20);
			this.sbFileList.SizingGrip = false;
			this.sbFileList.TabIndex = 17;
			// 
			// sbpFilesListed
			// 
			this.sbpFilesListed.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpFilesListed.Text = "[files]";
			this.sbpFilesListed.Width = 56;
			// 
			// lblWaiting
			// 
			this.lblWaiting.Location = new System.Drawing.Point(408, 0);
			this.lblWaiting.Name = "lblWaiting";
			this.lblWaiting.Size = new System.Drawing.Size(112, 16);
			this.lblWaiting.TabIndex = 10;
			this.lblWaiting.Text = "Generating file list ...";
			this.lblWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlFileListProgress
			// 
			this.pnlFileListProgress.Controls.Add(this.lblFileList);
			this.pnlFileListProgress.Controls.Add(this.pbFileList);
			this.pnlFileListProgress.Controls.Add(this.btnCancelFileList);
			this.pnlFileListProgress.Location = new System.Drawing.Point(40, 0);
			this.pnlFileListProgress.Name = "pnlFileListProgress";
			this.pnlFileListProgress.Size = new System.Drawing.Size(288, 24);
			this.pnlFileListProgress.TabIndex = 17;
			// 
			// lblFileList
			// 
			this.lblFileList.Location = new System.Drawing.Point(8, 4);
			this.lblFileList.Name = "lblFileList";
			this.lblFileList.Size = new System.Drawing.Size(136, 16);
			this.lblFileList.TabIndex = 16;
			this.lblFileList.Text = "Processing ...";
			// 
			// pbFileList
			// 
			this.pbFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pbFileList.Location = new System.Drawing.Point(144, 8);
			this.pbFileList.Name = "pbFileList";
			this.pbFileList.Size = new System.Drawing.Size(64, 8);
			this.pbFileList.TabIndex = 16;
			// 
			// btnCancelFileList
			// 
			this.btnCancelFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelFileList.Location = new System.Drawing.Point(216, 2);
			this.btnCancelFileList.Name = "btnCancelFileList";
			this.btnCancelFileList.Size = new System.Drawing.Size(68, 20);
			this.btnCancelFileList.TabIndex = 10;
			this.btnCancelFileList.Text = "Cancel";
			this.btnCancelFileList.Click += new System.EventHandler(this.btnCancelFileList_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(120, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 513);
			this.splitter1.TabIndex = 15;
			this.splitter1.TabStop = false;
			// 
			// pnlFileListLeft
			// 
			this.pnlFileListLeft.Controls.Add(this.groupBox1);
			this.pnlFileListLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlFileListLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlFileListLeft.Name = "pnlFileListLeft";
			this.pnlFileListLeft.Size = new System.Drawing.Size(120, 513);
			this.pnlFileListLeft.TabIndex = 14;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel10);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(104, 497);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// panel10
			// 
			this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel10.Controls.Add(this.panel12);
			this.panel10.Controls.Add(this.panel8);
			this.panel10.Controls.Add(this.panel6);
			this.panel10.Location = new System.Drawing.Point(8, 16);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(88, 473);
			this.panel10.TabIndex = 15;
			// 
			// panel12
			// 
			this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel12.Controls.Add(this.btnListFiles);
			this.panel12.Location = new System.Drawing.Point(0, 449);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(88, 24);
			this.panel12.TabIndex = 11;
			// 
			// btnListFiles
			// 
			this.btnListFiles.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnListFiles.Location = new System.Drawing.Point(4, 0);
			this.btnListFiles.Name = "btnListFiles";
			this.btnListFiles.Size = new System.Drawing.Size(80, 23);
			this.btnListFiles.TabIndex = 10;
			this.btnListFiles.Text = "List Files ->";
			this.btnListFiles.Click += new System.EventHandler(this.btnListFiles_Click);
			// 
			// panel8
			// 
			this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel8.Controls.Add(this.label1);
			this.panel8.Controls.Add(this.btnCheckAll);
			this.panel8.Controls.Add(this.btnUncheckAll);
			this.panel8.Location = new System.Drawing.Point(0, 0);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(88, 80);
			this.panel8.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 48);
			this.label1.TabIndex = 2;
			this.label1.Text = "These are the file types in this volume. Select the types to add to file list";
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Location = new System.Drawing.Point(8, 56);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(18, 18);
			this.btnCheckAll.TabIndex = 11;
			this.btnCheckAll.Text = "*";
			this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// btnUncheckAll
			// 
			this.btnUncheckAll.Location = new System.Drawing.Point(32, 56);
			this.btnUncheckAll.Name = "btnUncheckAll";
			this.btnUncheckAll.Size = new System.Drawing.Size(18, 18);
			this.btnUncheckAll.TabIndex = 10;
			this.btnUncheckAll.Text = "-";
			this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel6.Controls.Add(this.clbFileTypes);
			this.panel6.Location = new System.Drawing.Point(0, 80);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(88, 369);
			this.panel6.TabIndex = 0;
			// 
			// clbFileTypes
			// 
			this.clbFileTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clbFileTypes.CheckOnClick = true;
			this.clbFileTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clbFileTypes.Location = new System.Drawing.Point(0, 0);
			this.clbFileTypes.Name = "clbFileTypes";
			this.clbFileTypes.Size = new System.Drawing.Size(88, 362);
			this.clbFileTypes.TabIndex = 0;
			this.clbFileTypes.ThreeDCheckBoxes = true;
			// 
			// tpProperties
			// 
			this.tpProperties.Controls.Add(this.groupBoxXP2);
			this.tpProperties.Location = new System.Drawing.Point(4, 22);
			this.tpProperties.Name = "tpProperties";
			this.tpProperties.Size = new System.Drawing.Size(656, 532);
			this.tpProperties.TabIndex = 6;
			this.tpProperties.Text = "Properties";
			// 
			// groupBoxXP2
			// 
			this.groupBoxXP2.Controls.Add(this.usrcVolumeProperties);
			this.groupBoxXP2.Controls.Add(this.panel3);
			this.groupBoxXP2.Controls.Add(this.label5);
			this.groupBoxXP2.Controls.Add(this.lblDescription);
			this.groupBoxXP2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxXP2.Location = new System.Drawing.Point(0, 0);
			this.groupBoxXP2.Name = "groupBoxXP2";
			this.groupBoxXP2.Size = new System.Drawing.Size(656, 532);
			this.groupBoxXP2.TabIndex = 0;
			this.groupBoxXP2.TabStop = false;
			// 
			// usrcVolumeProperties
			// 
			this.usrcVolumeProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.usrcVolumeProperties.Location = new System.Drawing.Point(16, 422);
			this.usrcVolumeProperties.Name = "usrcVolumeProperties";
			this.usrcVolumeProperties.Size = new System.Drawing.Size(288, 96);
			this.usrcVolumeProperties.TabIndex = 18;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.txtDescription);
			this.panel3.Location = new System.Drawing.Point(16, 40);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(624, 350);
			this.panel3.TabIndex = 17;
			// 
			// txtDescription
			// 
			this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDescription.Location = new System.Drawing.Point(0, 0);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(622, 348);
			this.txtDescription.TabIndex = 0;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Location = new System.Drawing.Point(16, 406);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(288, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "Summary of directory structure data held in this volume :";
			// 
			// lblDescription
			// 
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblDescription.Location = new System.Drawing.Point(16, 24);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(624, 16);
			this.lblDescription.TabIndex = 6;
			this.lblDescription.Text = "You can enter a description for this volume here :";
			// 
			// pbVolumeLoad
			// 
			this.pbVolumeLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pbVolumeLoad.Location = new System.Drawing.Point(16, 275);
			this.pbVolumeLoad.Name = "pbVolumeLoad";
			this.pbVolumeLoad.Size = new System.Drawing.Size(640, 8);
			this.pbVolumeLoad.TabIndex = 1;
			// 
			// lblLoadProgressText
			// 
			this.lblLoadProgressText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLoadProgressText.Location = new System.Drawing.Point(16, 263);
			this.lblLoadProgressText.Name = "lblLoadProgressText";
			this.lblLoadProgressText.Size = new System.Drawing.Size(640, 12);
			this.lblLoadProgressText.TabIndex = 0;
			this.lblLoadProgressText.Text = "Loading volume ...";
			this.lblLoadProgressText.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// panelGallery1
			// 
			this.panelGallery1.ActivePanelIndex = 1;
			this.panelGallery1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panelGallery1.BackColor = System.Drawing.SystemColors.Control;
			this.panelGallery1.Controls.Add(this.pnlLoadInfo);
			this.panelGallery1.Controls.Add(this.pnlContent);
			this.panelGallery1.Location = new System.Drawing.Point(0, 0);
			this.panelGallery1.Name = "panelGallery1";
			this.panelGallery1.Panels.Add(this.pnlLoadInfo);
			this.panelGallery1.Panels.Add(this.pnlContent);
			this.panelGallery1.Size = new System.Drawing.Size(664, 558);
			this.panelGallery1.TabIndex = 18;
			// 
			// pnlLoadInfo
			// 
			this.pnlLoadInfo.Controls.Add(this.pbVolumeLoad);
			this.pnlLoadInfo.Controls.Add(this.lblLoadProgressText);
			this.pnlLoadInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlLoadInfo.Location = new System.Drawing.Point(0, 0);
			this.pnlLoadInfo.Name = "pnlLoadInfo";
			this.pnlLoadInfo.Size = new System.Drawing.Size(664, 558);
			this.pnlLoadInfo.TabIndex = 0;
			// 
			// pnlContent
			// 
			this.pnlContent.Controls.Add(this.tcVolume);
			this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContent.Location = new System.Drawing.Point(0, 0);
			this.pnlContent.Name = "pnlContent";
			this.pnlContent.Size = new System.Drawing.Size(664, 558);
			this.pnlContent.TabIndex = 0;
			// 
			// VolumeBrowser
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(672, 558);
			this.Controls.Add(this.panelGallery1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(632, 512);
			this.Name = "VolumeBrowser";
			this.Load += new System.EventHandler(this.VolumeBrowser_Load);
			this.tcVolume.ResumeLayout(false);
			this.tpExplorer.ResumeLayout(false);
			this.gbExplorer.ResumeLayout(false);
			this.tpFileList.ResumeLayout(false);
			this.gbFileList.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.pnlFileListStatus.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesListed)).EndInit();
			this.pnlFileListProgress.ResumeLayout(false);
			this.pnlFileListLeft.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel10.ResumeLayout(false);
			this.panel12.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.tpProperties.ResumeLayout(false);
			this.groupBoxXP2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panelGallery1.ResumeLayout(false);
			this.pnlLoadInfo.ResumeLayout(false);
			this.pnlContent.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		#endregion
		

		#region FIELDS Winform stuff

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TabControl tcVolume;
		private vcFramework.Windows.Forms.ListViewSP lvAllFiles;
		private GroupBox groupBoxXP2;
		private System.Windows.Forms.Label lblLoadProgressText;
		private System.Windows.Forms.ProgressBar pbVolumeLoad;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.ProgressBar pbFileList;
		private System.Windows.Forms.Label lblFileList;
		private System.Windows.Forms.GroupBox gbExplorer;
		private System.Windows.Forms.GroupBox gbFileList;
		private System.Windows.Forms.Label label5;
		private ProgressBarHelper m_objFileListGeneratorProgressBarHelper;
		private vcFramework.Windows.Forms.PanelGallery panelGallery1;
		private System.Windows.Forms.Panel pnlLoadInfo;
		private System.Windows.Forms.Panel pnlContent;
		private System.Windows.Forms.TabPage tpExplorer;
		private System.Windows.Forms.TabPage tpFileList;
		private System.Windows.Forms.TabPage tpProperties;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.CheckedListBox clbFileTypes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel pnlFileListStatus;
		private System.Windows.Forms.StatusBar sbFileList;
		private System.Windows.Forms.StatusBarPanel sbpFilesListed;
		private ContextMenu cmFileListMenu;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel4;
		private BrowseMonkey.VolumeProperties usrcVolumeProperties;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Panel pnlFileListLeft;
		private System.Windows.Forms.Label lblWaiting;
		private System.Windows.Forms.Panel pnlFileListProgress;

		#endregion


		#region FIELDS

		/// <summary> 
		/// Holds Treepath which should be focused 
		/// </summary>
		private string _focusPath;
		
		/// <summary> 
		/// Used to hold all threads started on this form 
		/// </summary>
		private ThreadCollection _threadCollection;

		/// <summary> 
		/// Stores file path and name for volume file which this browser was instantiated for.
		/// </summary>
		private string _volumePath;
		
		/// <summary> 
		/// Set to true if this form contains unsaved changes 
		/// </summary>
		private bool _dirty;
		
		/// <summary> 
		/// 
		/// </summary>
		public event EventHandler OnDirtyChanged;
		
		/// <summary> 
		/// Used to advance "load" progress bar based on time 
		/// </summary>
		private TimedProgressBarAdvancer _progressAdvancer;
		
		/// <summary> 
		/// Used to build list of files in file list mode
		/// </summary>
		private FSXmlFileListBuilder _fileListBuilder;
		
		/// <summary>
		/// 
		/// </summary>
		private FSXmlWinUI.FSXmlExplorer fsXmlExplorer;
		
		/// <summary>
		/// 
		/// </summary>
		private Volume _volume;
		
		/// <summary>
		/// The name this form will present. Is mostly cosmetic in nature, but 
		/// is also used when "exporting" the volume contents from this form. 
		/// Export dump forms require some kind of user-friendly "source" name
		/// so the user can identify which volume browser the export was done
		/// from. _forcedName is used for that
		/// 
		/// Forcedname can be 
		/// a) "New volume X" where X = 1 + the number of other new volumes  ;
		/// is used only for newly-created volumes that are not yet saved to 
		/// file
		/// 
		/// b) Volume file path file name
		/// 
		/// Note that when the form contents are Dirty, a "* " usually prepended
		/// to the form's displayed name. We store the forced name in its own
		/// field to make it easier to revert to the original name when the
		/// form is no longer Dirty.
		/// </summary>
		private string _forcedName;

		/// <summary>
		/// Used when populating listview for files list
		/// </summary>
		private ListViewItem[] _rows;

		/// <summary>
		/// produced from file list process
		/// </summary>
		private XmlDocument _filesListed;

		/// <summary> 
		/// List of  formats volume data can be dumped to
		/// </summary>
		private enum DumpFormats : int
		{
			HTMLTabulated,
			StructuredASCII,
			TabDelimitedASCII,
			XML
		}


		/// <summary>
		/// Set to true when cancelling file listing
		/// </summary>
		private bool _cancelFileList;
		
		/// <summary>
		/// Used to calculate the approx. time taken to load a volume
		/// </summary>
		private DateTime _loadStartTime;
		private System.Windows.Forms.Button btnDumpFileList;
		private System.Windows.Forms.Button btnCancelFileList;
		private System.Windows.Forms.Button btnListFiles;
		private System.Windows.Forms.Button btnUncheckAll;
		private System.Windows.Forms.Button btnCheckAll;
		
		/// <summary>
		/// Set to true if form is closing. used to abort SOME background thread 
		/// processes in the event of form shutting down while those processes
		/// are still running
		/// </summary>
		private bool _closing;

		#endregion


		#region PROPERTIES

		/// <summary> 
		/// Gets or sets if form has unsaved data on it 
		/// </summary>
		public bool Dirty
		{
			set
			{

				bool blnPreviousDirty = _dirty;
				_dirty = value;

				
				// ##############################################################
				// fires event only when the new value is different
				// from current value, ie, when the value will
				// change
				// --------------------------------------------------------------
				if (value != blnPreviousDirty)
					DelegateLib.InvokeSubscribers(
						OnDirtyChanged,
						this);


				// ##############################################################
				// Need to get the title text of this form. Normally this would
				// just be the filename of the volume, but newly-created vols
				// dont yet have filenames - in that case we use a constant 
				// label
				// --------------------------------------------------------------
				this.Text = this.ForcedName;
				if (_dirty)
					this.Text =  "* " + this.Text;



			}
			get
			{
				return _dirty;
			}
		}

		
		/// <summary>
		///  
		/// </summary>
		public string VolumePath
		{
			get
			{
				return _volumePath;
			}
		}

		
		/// <summary>
		/// Gets the instance of the volume which this browser is showing
		/// </summary>
		public Volume Volume
		{
			get
			{
				return _volume;
			}
		}

		
		/// <summary>
		/// Gets the forced name of this form
		/// </summary>
		public string ForcedName
		{
			get
			{
				return _forcedName;
			}
		}

	
		#endregion


		#region CONSTRUCTORS


		/// <summary> 
		/// 
		/// </summary>
		/// <param name="strFilePathAndName"></param>
		public VolumeBrowser(
			string volumePath,
			string focusPath
			)
		{
			
			// Important : normally we avoid putting logic in the constructor -
			// the Onload eventhandler is better for handling logic. but in the
			// case of this form, we want to draw the form AFTER we have loaded
			// the volume, incase the volume is invalid and the form must be
			// shutdown immediately - we can thus avoid drawign the form and then
			// having to close it again (confusing for user and looks sloppy). 
			// Therefore the constructor of this form calls the volume opening
			// method directly
			try
			{
			

				InitializeComponent();
				
				
				// stores values in members
				_volumePath = volumePath;
				_focusPath = focusPath;	

		
				CommonConstructorLogic();


				// ##################################################
				// add volume to "recent volumes" collection
				// --------------------------------------------------
				MainForm.RecentVolumes.Add(
					_volumePath);

				
				// populates volume info fields
				_forcedName = Path.GetFileName(_volumePath);
				this.Text = _forcedName;

			}
			catch
			{

				MainForm.ConsoleAdd(
					"An unexpected error occurred. " + Path.GetFileName(_volumePath) + " cannot be opened.");
				
				// if an error occurred here in the constructor, we 
				// need to force the browser form to shut down.
				this.Dispose();
			}

		}



		/// <summary>
		/// Constructor for volume browser taking a volume object. This would be a volume
		/// that's been opened elsewhere, normally in the volume creator. This browser 
		/// would then be the sole fixed reference to that volume, and if this browser is
		/// closed without saving the volume, it would be lost.
		/// </summary>
		/// <param name="volume"></param>
		public VolumeBrowser(
			Volume volume
			)
		{

			try
			{
				InitializeComponent();

				_volume = volume;
				_focusPath = "";

				CommonConstructorLogic();

				MainForm.UnsavedVolumesCount ++;
				_forcedName = "New Volume-" + MainForm.UnsavedVolumesCount;
				this.Text = _forcedName;
				this.Dirty = true;

			}
			catch
			{
				MainForm.ConsoleAdd(
					"An unexpected error occurred. Volume can not be opened.");
				
				// if an error occurred here in the constructor, we 
				// need to force the browser form to shut down.
				this.Dispose();				
			}
		}



		/// <summary>
		/// 
		/// </summary>
		private void CommonConstructorLogic(
			)
		{
			
			_threadCollection = new ThreadCollection();

			this.Dirty = false;
			this.Closing += new CancelEventHandler(VolumeBrowser_Closing);
			// sets structure of listviews
			lvAllFiles.ColumnsSet(
				MainForm.XmlListViewConfig.SelectSingleNode(".//listview [@name='volumeBrowser_Files']").ChildNodes);
			lvAllFiles.RowCountChanged += new EventHandler(FileList_CountChanged);
			lvAllFiles.DoubleClick += new EventHandler(FileList_DoubleClick);
			
			lvAllFiles.MultiSelect		= true;
			lvAllFiles.InsertPosition	= ListViewSP.InsertPositions.Bottom;
			pnlFileListProgress.Dock	= DockStyle.Fill;
			sbFileList.Dock				= DockStyle.Fill;
			lvAllFiles.Dock				= DockStyle.Fill;
			lblWaiting.Dock				= DockStyle.Fill;
			pnlFileListProgress.Visible	= false;
			lblWaiting.Visible			= false;


			// #####################################################
			// builds context menu for files listview
			// -----------------------------------------------------
			cmFileListMenu = new ContextMenu();
			MenuItem mnuExportFileList = new MenuItem("Export as text");
			mnuExportFileList.Click += new EventHandler(mnuExportFileList_Click);  
			cmFileListMenu.MenuItems.Add(mnuExportFileList);
			lvAllFiles.ContextMenu = cmFileListMenu;


			// #####################################################
			// restore state
			// -----------------------------------------------------
			if (MainForm.StateBag.Contains("VolumeBrowser.FileListLeft"))
				pnlFileListLeft.Width = (int)MainForm.StateBag.Retrieve(
					"VolumeBrowser.FileListLeft");


			// force an event to set text
			FileList_CountChanged(null,null);

		}



		#endregion


		#region DESTRUCTORS
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(
			bool disposing 
			)
		{
			try
			{

				// #####################################################
				// prompt user to save/not save/cancel
				// -----------------------------------------------------
				if (this.Dirty)
				{

					DialogResult result = MessageBox.Show(
						"Save changes to " + _forcedName + " before closing?", 
						Application.ProductName, 
						MessageBoxButtons.YesNoCancel);

					if(result == DialogResult.Yes)
					{
						this.Save();
						// checks if form is still dirty after save - if so, 
						// means save was cancelled. therefore abort dispose
						if (this.Dirty)
							return;
					}
					else if(result == DialogResult.Cancel)
						return;

				}


				if(disposing)
				{
					if (_volume != null)
						_volume.Dispose();
				
					// destroys all threads started on this object
					_threadCollection.AbortAndRemoveAllThreads();
				
					// stops progress bar timer if its running
					if (_progressAdvancer != null)
						_progressAdvancer.Stop();

					
					// #####################################################
					// saves the state of listviews - note that listview on 
					// usercontrol (treeview associated) is not saved, due 
					// to its protection level (member of usercontrold, not 
					// this form)
					// -----------------------------------------------------
					MainForm.StateBag.Add(
						"VolumeBrowser.FileListLeft", 
						pnlFileListLeft.Width);

					if(components != null)
						components.Dispose();

				}
			
				base.Dispose(disposing);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}

		
		#endregion

		
		#region METHODS - Volume Load
		
		/// <summary>
		/// Starts populating browser with currently specified volume
		/// </summary>
		private void LoadVolume(
			)
		{

			// focus panel containing populate progress bar
			panelGallery1.ActivePanelIndex	= 0;



			// ##################################################
			// calculates projected load time for volume
			// --------------------------------------------------
			if (_volumePath != null)
			{
				FileInfo file = new FileInfo(_volumePath);

				// retrieves the last recorded load rate, if it exists
				double speed = 0;	
				if (MainForm.StateBag.Contains("VolumeBrowser.volumeLoadRate"))
					speed = (double)MainForm.StateBag.Retrieve(
						"VolumeBrowser.volumeLoadRate");

				//if (speed == Double.NaN || speed == Double.NegativeInfinity || speed == Double.PositiveInfinity)
				if (speed >= 0)
				{}
				else
				{speed = 0;}

				// gets load time and starts time progressbar
				int loadDuration = (int)Math.Round(speed * (file.Length/1024),0);

				_progressAdvancer = new TimedProgressBarAdvancer(
					loadDuration,
					pbVolumeLoad);
				_progressAdvancer.Start();


				_loadStartTime = DateTime.Now;
			}


			// #######################################################
			// invokes method which popualtes all "default" stuff on 
			// this form, in a new thread
			// -------------------------------------------------------
			Thread th		= new Thread(new ThreadStart(LoadVolume_BackgroundProcess));
			th.Name			= "PopulateBrowser";
			th.IsBackground = true;

			_threadCollection.AddThread(th);
			th.Start();

		}

		
		
		/// <summary> 
		/// Handles all pre-GUI population stuff. This is done in a background process 
		/// to prevent locking GUI up. When these processes are done, GUI updating is 
		/// invoked. This method is invoked from this form's onload event.
		/// </summary>
		private void LoadVolume_BackgroundProcess(
			)
		{

			WinFormActionDelegate guiInvoker		= null;

			// opens volume object using file path, if volume is null. if volume is not
			// null it's because volume object has been passed in from somewhere else, 
			// like a volume creation dialog
			if (_volume == null)
			{
				try
				{
					// open volume object
					_volume = new Volume(
						_volumePath);

					// force loading of volume data to ensure validity
					XmlDocument d = _volume.VolumeData;
			
				}
				catch
				{
					// exception can be thrown if closing browser 
					// down. if so simply exit entirely
					if (_closing)
						return;

					MainForm.ConsoleAdd(
						"Unable to open " + Path.GetFileName(_volumePath));



					// must shutdown browser form, but because this is being done from
					// another thread, must use invoke
					guiInvoker = new WinFormActionDelegate(
						this.Dispose);


					this.Invoke(
						guiInvoker);
				
					// return nothing to ensure a delayed Dispose doesn't 
					// lead to proceeding code being reached
					return;
				}

				// stops timer-based progress bar advancer
				_progressAdvancer.Stop();

			}// if


			// ##################################################
			// must now update GUI - to make this thread-safe, 
			// have to call the method which does updating useing 
			// a delegate
			// --------------------------------------------------
			// exception can be thrown if closing browser 
			// down. if so simply exit entirely
			if (_closing)
				return;

			guiInvoker = new WinFormActionDelegate(LoadVolume_GUIProcess);
			this.Invoke(guiInvoker);

		}
		

		
		/// <summary> 
		/// Continues the Volume Browser populating process, handling all GUI-related 
		/// process. This method is typically called from a background thread, and 
		/// must therefore be invoked using a delegate instead of being called directly 
		/// </summary>
		private void LoadVolume_GUIProcess(
			)
		{

			// ##################################################
			// invokes population on XmlFileStructure explorer 
			// usercontrol. focuses path if necessary
			// --------------------------------------------------
			fsXmlExplorer.Initialize(_volume.VolumeData);

			if (_focusPath.Length > 0)
				fsXmlExplorer.FocusPath(
					_focusPath);


			// ##################################################
			// stores the rate (milliseconds/kb) observed opening 
			// the volume
			// --------------------------------------------------
			if (_volumePath != null)
			{
				TimeSpan duration = DateTime.Now - _loadStartTime;
				FileInfo file = new FileInfo(_volumePath);
			
				MainForm.StateBag.Add(
					"VolumeBrowser.volumeLoadRate",
					Convert.ToDouble(duration.Seconds)/Convert.ToDouble(file.Length/1024));

				pbVolumeLoad.Value = pbVolumeLoad.Maximum;
			}


			// ##################################################
			// set misc. small things which dont take long
			// --------------------------------------------------
			panelGallery1.ActivePanelIndex	= 1;
			txtDescription.Text				= _volume.Description;
			
			// display properites
			usrcVolumeProperties.DisplayProperties(_volume);	


			// ##################################################
			// fills list of all file extensions
			// --------------------------------------------------
			ListBoxLib.PopulateListBox(
				(ListBox)clbFileTypes, 
				_volume.FileTypes);


		}



		#endregion


		#region METHODS - File List


		/// <summary> 
		/// Populates AllFiles list view using member xml data in this Browser - 
		/// as new thread
		/// </summary>
		private void ListContents_Start(
			)
		{
	
			// creates object used to generate list of files
			_fileListBuilder = new FSXmlFileListBuilder(
				_volume.VolumeData.DocumentElement);

			string[] fileTypes = new string[clbFileTypes.CheckedItems.Count];
			for (int i = 0 ; i < clbFileTypes.CheckedItems.Count ; i ++ )
			{
				if (clbFileTypes.CheckedItems[i] is DisplayValueItem)
				{
					DisplayValueItem item = (DisplayValueItem)clbFileTypes.CheckedItems[i];
					fileTypes[i] = item.ValueMember;
				}
				else if (clbFileTypes.CheckedItems[i] is string)
					fileTypes[i] = clbFileTypes.CheckedItems[i].ToString();
			}


			_fileListBuilder.IncludedFileTyYpes = fileTypes;



			// sets up progress bar helper
			m_objFileListGeneratorProgressBarHelper = new ProgressBarHelper(
				pbFileList,
				_fileListBuilder);
			
			lvAllFiles.Items.Clear();
			
			// workaround - event still not firing, so invoke manually
			FileList_CountChanged(null,null);
			

			lblFileList.Text = "(1/2) Finding files";

			lvAllFiles.Visible			= false;
			lblWaiting.Visible			= true;
			btnCheckAll.Enabled			= false;
			btnUncheckAll.Enabled		= false;
			clbFileTypes.Enabled		= false;
			btnListFiles.Enabled		= false;
			sbFileList.Visible			= false;
			lblWaiting.Visible			= false;
			pnlFileListProgress.Visible	= true;
	
			_cancelFileList				= false;

			

			Thread th					= new Thread(new ThreadStart(ListContents_BackgroundProcess));
			th.Name						= "FillAllFilesListView";	// this name must not be changed, as it is used later for possible thread terminations
			th.IsBackground				= true;
			
			_threadCollection.AddThread(th);

			th.Start();
		}
		



		/// <summary> 
		/// Does all background processing for file listing. This is mainly the 
		/// generation of an Xml document containing data from the VolumeDate 
		/// object - the files selected are based on list arguments set by the 
		/// user on the form. 
		/// </summary>
		private void ListContents_BackgroundProcess(
			)
		{
			
			WinFormActionDelegate guiInvoker = null;


			// gets list of files in xmldocument form - this is 
			// done as a background process
			_rows = new ListViewItem[0];
			_fileListBuilder.Start();
			_filesListed = _fileListBuilder.OutputXml;

	
			// ############################################
			// aborts process if necessary - this is done
			// by simply avoiding the process which builds 
			// up listview contents
			// --------------------------------------------
			if (_cancelFileList)
			{
				guiInvoker = new WinFormActionDelegate(
					ListContents_GUIProcess);
				this.Invoke(guiInvoker);	
				return;
			}
			



			guiInvoker = new WinFormActionDelegate(
				ListContents_UpdateGUI);
			this.Invoke(guiInvoker);	


			// ############################################
			// note that we dont check for cancelling in 
			// this loop, purely because it saves addng an 
			// additional if evaulation in what is already 
			// a fairly "heavy" loop. that means that once 
			// we start building the the listview rows up, 
			// we we cannot stop that process
			// --------------------------------------------
			_rows = new ListViewItem[_filesListed.DocumentElement.ChildNodes.Count];

			for(int i = 0 ; i < _filesListed.DocumentElement.ChildNodes.Count ; i ++)
			{
				XmlNode file = _filesListed.DocumentElement.ChildNodes[i];
				
				string[] rowContents = new string[5];
				rowContents[0] = file.SelectSingleNode("n").InnerText;
				rowContents[1] = file.SelectSingleNode("dc").InnerText;
				rowContents[2] = file.SelectSingleNode("dm").InnerText;
				rowContents[3] = file.SelectSingleNode("s").InnerText;
				rowContents[4] = file.SelectSingleNode("o/itemPath").InnerText;
				
				_rows[i] = new ListViewItem(rowContents);
				
				m_objFileListGeneratorProgressBarHelper.PerformStep();
			}



			// ############################################
			// --------------------------------------------
			guiInvoker = new WinFormActionDelegate(
				ListContents_GUIProcess);
			
			this.Invoke(guiInvoker);	
			//ListContents_GUIProcess(); //for debug only. calls method directly
		

		}
		
		
		/// <summary>
		/// Invoked in the middle of the file list generation process, AFTER the
		/// files to be listed have been obtained, but BEFORE the listview rows
		/// have been generated from those files.
		/// </summary>
		private void ListContents_UpdateGUI(
			)
		{
			
			lblFileList.Text = "(2/2) Building list";

			m_objFileListGeneratorProgressBarHelper = new ProgressBarHelper(
				pbFileList,
				_filesListed.DocumentElement.ChildNodes.Count);

		}


		/// <summary>  
		/// 
		/// </summary>
		private void ListContents_GUIProcess(
			)
		{

			if (_cancelFileList)
				return;

			pnlFileListProgress.Visible	= false;
			lblWaiting.Visible	= true;

			Application.DoEvents();

			if (_rows.Length > 0)
				lvAllFiles.Items.AddRange(_rows);

			// need to explicitly set count because addrange doesnt fire
			// row count changed event
			FileList_CountChanged(null,null);

			// set row colors on listview
			lvAllFiles.SetRowColors(
				Color.FromArgb(242,242,242),
				Color.FromArgb(253,253,253));

			btnCheckAll.Enabled			= true;
			btnUncheckAll.Enabled		= true;
			btnListFiles.Enabled		= true;
			clbFileTypes.Enabled		= true;

			lvAllFiles.Visible			= true;
			lblWaiting.Visible			= false;
			sbFileList.Visible			= true;


		}



		#endregion


		#region METHODS - MISC



		/// <summary>
		/// Wraps the saving process for a volume object.
		/// </summary>
		public void Save(
			)
		{
		
			// writes all form contents to volume
			_volume.Description = txtDescription.Text;

			// saves the volume
			if (_volume.BoundToFile)
				_volume.Save();
			else
			{

				SaveFileDialog dialog = new SaveFileDialog();
				dialog.Filter = Constants.FILE_DIALOGUE_FILTER;
				dialog.ShowDialog();

				string savePath = dialog.FileName;
				
				if (savePath.Length == 0)
					return;
				
				_volume.Save(
					savePath);
			}	


			// ##################################################
			// reset form dirty status
			// --------------------------------------------------
			_forcedName = Path.GetFileName(_volume.Path);
			this.Text = _forcedName;

			// force form to clean again
			this.Dirty = false;

			// display properites
			usrcVolumeProperties.DisplayProperties(_volume);	


			// ##################################################
			// add volume to "recent volumes" collection
			// --------------------------------------------------
			MainForm.RecentVolumes.Add(
				_volume.Path);

		}
		


		/// <summary>
		/// Wraps the "save as" process for a volume object. Is similar to plain "Save()",
		/// but differs in that it always throws up a file path dialogue, and if the 
		/// volume object in this browser is already bound to a file, but a new file path
		/// is selected in dialogue, the volume object is recreated on that new path.
		/// </summary>
		public void SaveAs(
			)
		{

			// writes all form contents to volume
			_volume.Description = txtDescription.Text;


			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = Constants.FILE_DIALOGUE_FILTER;
			dialog.ShowDialog();

			string savePath = dialog.FileName;
				
			if (savePath.Length == 0)
				return;

			// ###########################################
			// checks if the new specified filepath is in
			// use by another browser
			// -------------------------------------------
			VolumeBrowser[] browsers = FormFinderLib.GetVolumeBrowsers();
			foreach(VolumeBrowser browser in browsers)
				if (browser.VolumePath == savePath && this.VolumePath != savePath)
				{
					MainForm.ConsoleAdd(
						"Could not overwrite '"+ savePath + "' because that file " +
						"is in use. Please close that file first, or specify a different save path");
					return;
				}



			// ###########################################
			// checks if the new specified filepath is
			// writeable
			// -------------------------------------------
			if (File.Exists(savePath))
			{
				try
				{
					FileStream s = new FileStream(savePath,FileMode.Open);
					s.Close();
				}
				catch
				{
					MainForm.ConsoleAdd("Save failed. Is the target path locked by another process?");
					return;
				}
			}


			_volume.Save(savePath);


			// ##################################################
			// reset form dirty status
			// --------------------------------------------------
			_volumePath = _volume.Path;
			_forcedName = Path.GetFileName(_volume.Path);
			this.Text = _forcedName;

			// force form to clean again
			this.Dirty = false;

			// display properites
			usrcVolumeProperties.DisplayProperties(_volume);	


			// ##################################################
			// add volume to "recent volumes" collection
			// --------------------------------------------------
			MainForm.RecentVolumes.Add(
				_volume.Path);

		}



		/// <summary> 
		/// Focuses the treeview object with a given string path 
		/// </summary>
		/// <param name="strPath"></param>
		public void FocusTreeNode(
			string strPath
			)
		{
			fsXmlExplorer.FocusPath(
				strPath);
		}



		#endregion


		#region EVENTS


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeBrowser_Load(
			object sender, 
			EventArgs e
			)
		{
			LoadVolume();
		}



		/// <summary>
		/// Invoked when form is closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeBrowser_Closing(
			object sender, 
			CancelEventArgs e
			)
		{
			_closing = true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuExportFileList_Click(
			object sender,
			EventArgs e
			)
		{
			FormFactory.SpawnFileListExporter(
				_forcedName,
				lvAllFiles);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileList_DoubleClick(
			object sender, 
			EventArgs e
			)
		{
			
			if (lvAllFiles.SelectedItems.Count == 0)
				return;

			string path = lvAllFiles.SelectedItems[0].SubItems[4].Text;
			
			tcVolume.SelectedIndex = 0;
			this.FocusTreeNode(path);

		}

		

		/// <summary>
		/// Invoked when the number of rows in files list view changes.
		/// Event not fired when doing an addrange insert, so the 
		/// event is raised manually 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileList_CountChanged(
			object sender, 
			EventArgs e
			)
		{
			sbpFilesListed.Text = "Files : " + lvAllFiles.Items.Count;

			btnDumpFileList.Enabled = false;
			if (lvAllFiles.Items.Count > 0)
				btnDumpFileList.Enabled = true;
		}


		
		/// <summary> 
		/// Invoked when text in txtDescription is changed, which can be done by user input, or
		/// onload of form too. Though this is a front-line event handler, note that it has
		/// no try-catch.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtDescription_TextChanged(
			object sender, 
			EventArgs e
			)
		{
			// by checking if field is focused, we ensure
			// that we're responding to user input, not
			// onload text input from the form itself
			if (txtDescription.Focused)
				this.Dirty = true;
		}



		/// <summary>
		/// Invoked when the volume is saved - saving is triggered from a source outside this
		/// volume browser. This event handler watches for saving, and updates the volumebrowser
		/// to reflect the saved changes
		///  </summary>
		private void OnVolumeSaved(
			object sender, 
			EventArgs e
			)
		{

			// updates the header on this form
			_forcedName = Path.GetFileName(
				_volume.Path);

			this.Text = _forcedName;

			// adds saved message to console
			MainForm.ConsoleAdd(
				"Saved " + Path.GetFileName(_volume.Path));

			// force form to clean again
			this.Dirty = false;


		}
		


		#endregion


		#region EVENTS - Buttons


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnListFiles_Click(
			object sender, 
			System.EventArgs e
			)
		{
			try
			{
				ListContents_Start();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}		
		}



		/// <summary> 
		/// When pressed, any ongoing file list threads are to be cancelled 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelFileList_Click(
			object sender, 
			System.EventArgs e
			)
		{
			try
			{
				
				_cancelFileList = true;

				// aborts file list thread. note this is a hardcoded reference, 
				// which is not ideal.
				_threadCollection.AbortAndRemoveThread(
					"FillAllFilesListView");

				

				// because thread can be aborted in the middle of a 
				// row being added to listview, a half a row of data
				// has been known to appear in listview - selecting
				// this "half row", or Frankenrow as I prefer to call
				// it, can throw a null object exception. Therefore
				// it is safer to just clear the listview.
				lvAllFiles.Items.Clear();



				// must hide the progress bar and cancel button stuff
				btnCheckAll.Enabled			= true;
				btnUncheckAll.Enabled		= true;
				btnListFiles.Enabled		= true;
				clbFileTypes.Enabled		= true;

				pnlFileListProgress.Visible	= false;
				lvAllFiles.Visible			= true;
				lblWaiting.Visible			= false;
				sbFileList.Visible			= true;


			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCheckAll_Click(
			object sender, 
			System.EventArgs e
			)
		{

			for (int i = 0 ; i < clbFileTypes.Items.Count ; i ++)
				clbFileTypes.SetItemChecked(i, true);

		}
	


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnUncheckAll_Click(
			object sender, 
			System.EventArgs e
			)
		{
			for (int i = 0 ; i < clbFileTypes.Items.Count ; i ++)
				clbFileTypes.SetItemChecked(i, false);		
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTextDump_Click(
			object sender, 
			System.EventArgs e)
		{
			FormFactory.SpawnFileListExporter(
				_forcedName,
				lvAllFiles);
		}


		#endregion


	}
}
