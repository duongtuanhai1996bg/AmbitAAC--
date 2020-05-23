using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbitAAC
{
    public partial class Form1 : Form
    {
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchData();
            }
        }

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchData();
            }
        }
        public void SearchData()
        {
            this.Cursor = Cursors.WaitCursor;
            string sql = "";
            if (rdNI.Checked)
            {                
                if (cbModel.Text == "")
                {
                    MessageBox.Show(this, "CHƯA CHỌN TÊN SẢN PHẨM/请选择项目");
                    return;
                }
                if (txtSN.Text != "" && txtLot.Text == "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where s.SN = '{0}' and s.IsRepair = '3' order by SN asc, s.Time desc", txtSN.Text.Trim());
                }
                else if (txtSN.Text == "" && txtLot.Text != "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where p.[Name] = '{0}' and s.IsRepair = '3' order by SN asc, s.Time desc", txtLot.Text.Trim());
                }
                else if (txtSN.Text != "" && txtLot.Text != "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where s.SN = '{0}' and s.IsRepair = '3' and p.[Name] = '{1}' order by SN asc, s.Time desc", txtSN.Text.Trim(), txtLot.Text.Trim());
                }
                else
                {
                    return;
                }                
            }
            else if (rdAT.Checked)
            {
                if (cbModel.Text == "")
                {
                    MessageBox.Show(this, "CHƯA CHỌN TÊN SẢN PHẨM/请选择项目");
                    return;
                }
                if (txtSN.Text != "" && txtLot.Text == "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where s.SN = '{0}' and s.IsRepair = '4' order by SN asc, s.Time desc", txtSN.Text.Trim());
                }
                else if (txtSN.Text == "" && txtLot.Text != "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where p.[Name] = '{0}' and s.IsRepair = '4' order by SN asc, s.Time desc", txtLot.Text.Trim());
                }
                else if (txtSN.Text != "" && txtLot.Text != "")
                {
                    sql = string.Format("SELECT p.[Name] as '批次',s.SN,REPLACE(REPLACE(IsPass,0,'NG'),1,'OK') as '结果',s.[Time] as '时间',p.FinishNum as '当前数量',p.Num as '批次数量',t.[Name] as '类型',r.[RouteName] as '通道',s.Details as '日志档' from T_SN_Test s inner join T_Pici p on s.PiciID=p.ID inner join [T_Process] t on s.ProcessID=t.ID inner join [tb_Route] r on s.IsRepair=r.RouteNO  where s.SN = '{0}' and s.IsRepair = '4' and p.[Name] = '{1}' order by SN asc, s.Time desc", txtSN.Text.Trim(), txtLot.Text.Trim());
                }
                else
                {
                    return;
                }
            }
            else if (rdPack.Checked)
            {
                if (!cbRework.Checked)
                {
                    if (txtSN.Text != "" && txtLot.Text == "")
                    {
                        sql = string.Format("select j.fJName as '型号', b.fBatchNo as '批次', b.fSN as 'SN', (case b.[fSum] when 0 then '重码' else 'OK' end) as '情况', b.fUserNo as '工号', u.fUserName as '用户名', b.fCheckDate as '时间' from [tBarCodeCheck] b inner join [tBarJect] j on b.fJId = j.fJId inner join [tBarSelectList] l on b.fLId = l.fLId inner join [tBaseUsers] u on b.fUserNo = u.fUserNo where b.fSN = '{0}'", txtSN.Text.Trim());
                    }
                    else if (txtSN.Text == "" && txtLot.Text != "")
                    {
                        sql = string.Format("select j.fJName as '型号', b.fBatchNo as '批次', b.fSN as 'SN', (case b.[fSum] when 0 then '重码' else 'OK' end) as '情况', b.fUserNo as '工号', u.fUserName as '用户名', b.fCheckDate as '时间' from [tBarCodeCheck] b inner join [tBarJect] j on b.fJId = j.fJId inner join [tBarSelectList] l on b.fLId = l.fLId inner join [tBaseUsers] u on b.fUserNo = u.fUserNo where b.fBatchNo = '{0}'", txtLot.Text.Trim());
                    }
                    else if (txtSN.Text != "" && txtLot.Text != "")
                    {
                        sql = string.Format("select j.fJName as '型号', b.fBatchNo as '批次', b.fSN as 'SN', (case b.[fSum] when 0 then '重码' else 'OK' end) as '情况', b.fUserNo as '工号', u.fUserName as '用户名', b.fCheckDate as '时间' from [tBarCodeCheck] b inner join [tBarJect] j on b.fJId = j.fJId inner join [tBarSelectList] l on b.fLId = l.fLId inner join [tBaseUsers] u on b.fUserNo = u.fUserNo where b.fSN = '{0}' and b.fBatchNo = '{1}'", txtSN.Text.Trim(), txtLot.Text.Trim());
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (txtSN.Text != "" && txtLot.Text == "")
                    {
                        sql = string.Format("select [Product] as '型号', [BatchNo] as '批次', [SN] as 'SN', [UserID] as '工号', [CheckDate] as '时间' from [T_SN_Check] with(nolock) where [SN] = '{0}'", txtSN.Text.Trim());
                    }
                    else if (txtSN.Text == "" && txtLot.Text != "")
                    {
                        sql = string.Format("select [Product] as '型号', [BatchNo] as '批次', [SN] as 'SN', [UserID] as '工号', [CheckDate] as '时间' from [T_SN_Check] with(nolock) where [BatchNo] = '{0}'", txtLot.Text.Trim() + "_1");
                    }
                    else if (txtSN.Text != "" && txtLot.Text != "")
                    {
                        sql = string.Format("select [Product] as '型号', [BatchNo] as '批次', [SN] as 'SN', [UserID] as '工号', [CheckDate] as '时间' from [T_SN_Check] with(nolock) where [SN] = '{0}' and [BatchNo] = '{1}'", txtSN.Text.Trim(), txtLot.Text.Trim() + "_1");
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (rdLQ.Checked)
            {
                if (txtSN.Text != "")
                {
                    sql = string.Format("SELECT s.[Product] as '型号',s.SN,s.Result as '结果',s.[WarnStation] as '机台',s.[TestTime] as '测试时间',s.[ServerTime] as '上传时间',s.[FileName] as '日志档' from [T_SnCollection] s with(nolock)  where s.SN = '{0}' order by SN asc, s.ServerTime desc", txtSN.Text.Trim());
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            dt = new DataTable();
            try
            {
                dt = SqlConn.sqlDataTable(sql);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeRows();
                dataGridView1.AutoResizeColumns();
                txtSN.Text = null;
                txtLot.Text = null;
                btnExport.Visible = true;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Get Data Fail: " + ex.Message);
                return;
            }
        }

        private void rdNI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNI.Checked)
            {
                cbModel.Visible = true;
                SqlConn.GetDbName = "SFC" + cbModel.Text;
                SqlConn.GetDataSource = "10.8.145.152";
                SqlConn.GetUserName = "o-sfc";
                SqlConn.GetPassWord = "o-sfc.aac";
            }
            else
                cbModel.Visible = false;
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdNI.Checked || rdAT.Checked)
            {
                SqlConn.GetDbName = "SFC" + cbModel.Text;
                SqlConn.GetDataSource = "10.8.145.152";
                SqlConn.GetUserName = "o-sfc";
                SqlConn.GetPassWord = "o-sfc.aac";
            }
        }        

        private void rdPack_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPack.Checked)
            {
                SqlConn.GetDbName = "WebOnlineCode";
                SqlConn.GetDataSource = "10.8.145.145";
                SqlConn.GetUserName = "o-sns";
                SqlConn.GetPassWord = "o-sns.aac";
                cbRework.Visible = true;
                cbRework.Checked = false;
            }
            else
            {
                cbRework.Visible = false;
                cbRework.Checked = false;
            }
        }

        private void rdLQ_CheckedChanged(object sender, EventArgs e)
        {
            if (rdLQ.Checked)
            {
                SqlConn.GetDbName = "sfcRelay";
                SqlConn.GetDataSource = "10.8.145.152";
                SqlConn.GetUserName = "o-sfc";
                SqlConn.GetPassWord = "o-sfc.aac";
                label2.Enabled = false;
                txtLot.Enabled = false;
            }
            else
            {
                label2.Enabled = true;
                txtLot.Enabled = true;
            }
        }
        private void cbRework_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbRework.Checked)
            {
                SqlConn.GetDbName = "WebOnlineCode";
            }
            else
            {
                SqlConn.GetDbName = "SFCOnlineCode";
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "Chọn đường dẫn lưu file...";
            string foldPath = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
            }
            else
            {
                return;
            }
            //保存进excel
            string fileName = "DATA--" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".xls";
            string fullPath = (foldPath + "\\" + fileName).Replace("\\\\", "\\");
            Excel.Export2Excel(dt, fullPath, false, Encoding.Unicode);
            MessageBox.Show(this, "Export to Excel OK");
            btnExport.Visible = false;
        }

        private void rdAT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAT.Checked)
            {
                cbModel.Visible = true;
                SqlConn.GetDbName = "SFC" + cbModel.Text;
                SqlConn.GetDataSource = "10.8.145.152";
                SqlConn.GetUserName = "o-sfc";
                SqlConn.GetPassWord = "o-sfc.aac";
            }
            else
                cbModel.Visible = false;
        }
    }
}
