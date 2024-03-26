using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrimexTransferencia
{
    class LotesEmbarque
    {
        public bool AsignarLotes(string  CentroCompraID,string DocumentoID, string Fecha, int CantidadSurtir,DataGridView Lotes,string ProductoID)
        {
            ConexionSql cn = new ConexionSql();
            bool _correcto = false;
            try
            {
                string consulta = "";
                int _tope = 1, _lotes = 0, _topeFilas = 0;
                bool _suficiente = false;
                SqlCommand cmd;
                SqlDataReader reader;
                int i = 0;

                consulta = "select top 1 indice from( " +
                    " select ROW_NUMBER()over(order by LOTE_EMBARQUE_FECHA_CREACION desc) as indice, * from LOTE_EMBARQUE" +
                    " ) as Indice order by indice desc";
                cn.ConectarSQLServer();
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    _topeFilas = Convert.ToInt32(reader["indice"]);
                reader.Close();
                cmd.Cancel();
                cn.Desconectar();
                while (_suficiente == false)
                {
                    consulta = "select sum(Lotes) as lotes from(select top " + _tope + " sum(Lote_embarque_cantidad) AS Lotes from LOTE_EMBARQUE " +
                        " where PRODUCTO_ID=" + ProductoID + "And DOCUMENTO_ID = "+ DocumentoID +" and embarque_id is null and Lote_embarque_cantidad is not null and Lote_embarque_cantidad>0" +
                        " group by LOTE_EMBARQUE_FECHA_CREACION" +
                        " order by LOTE_EMBARQUE_FECHA_CREACION desc) as aux";
                    cn.ConectarSQLServer();
                    cmd = new SqlCommand(consulta, cn.SC);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        _lotes = Convert.ToInt32(reader["Lotes"]);
                    reader.Close();
                    cmd.Cancel();
                    cn.Desconectar();
                    if (_lotes >= CantidadSurtir)
                        _suficiente = true;

                    else
                    if (_tope == _topeFilas)
                        break;
                        _tope++;
                }
                reader.Close();
                cmd.Cancel();
                cn.Desconectar();
                cn.ConectarSQLServer();
                if (_suficiente == true)
                {
                    Lotes.Rows.Clear();
                    consulta = "select TOP " + _tope + " LOTE_EMBARQUE_ID, LOTE_EMBARQUE_SERIE, LOTE_EMBARQUE_FECHA_CREACION, LOTE_EMBARQUE_USUARIO_CREA, " +
                        " TIPO_DOCUMENTO, DOCUMENTO_ID, EMBARQUE_ID, PRODUCTO_ID, LOTE_EMBARQUE_CANTIDAD from LOTE_EMBARQUE "+
                        " where DOCUMENTO_ID="+ DocumentoID + " and PRODUCTO_ID=" + ProductoID + " and embarque_id is null and Lote_embarque_cantidad is not null and Lote_embarque_cantidad>0" +
                        " order by LOTE_EMBARQUE_FECHA_CREACION desc";
                    cmd = new SqlCommand(consulta, cn.SC);
                    reader = cmd.ExecuteReader();
                    i = 0;
                    while (reader.Read())
                    {
                        Lotes.Rows.Add();
                        Lotes[0, i].Value = reader["LOTE_EMBARQUE_ID"].ToString();
                        Lotes[1, i].Value = reader["LOTE_EMBARQUE_SERIE"].ToString();
                        Lotes[2, i].Value = reader["LOTE_EMBARQUE_FECHA_CREACION"].ToString();
                        Lotes[3, i].Value = reader["LOTE_EMBARQUE_USUARIO_CREA"].ToString();
                        Lotes[4, i].Value = reader["TIPO_DOCUMENTO"].ToString();
                        Lotes[5, i].Value = reader["DOCUMENTO_ID"].ToString();
                        Lotes[6, i].Value = reader["PRODUCTO_ID"].ToString();
                        Lotes[7, i++].Value = reader["LOTE_EMBARQUE_CANTIDAD"].ToString();
                    }
                    reader.Close();
                    cmd.Cancel();
                    cn.Desconectar();
                    _correcto = true;
                }
            }
            catch (Exception Ex)
            {
                _correcto = false;
                MessageBox.Show(Ex.Message, "Error");
            }
            return _correcto;
        }
        public void ActualizarLotes(string EmbarqueID,DataGridView dGVLotes)
        {
            if (dGVLotes.Rows.Count > 0)
            {
                ConexionSql cn = new ConexionSql();
                try
                {
                    string consulta = "";
                    SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                    cn.ConectarSQLServer();
                    string _loteID = "";
                    for (int i = 0; i < dGVLotes.Rows.Count; i++)
                    {
                        _loteID = dGVLotes["LoteID", i].Value.ToString();
                        consulta = "UPDATE [dbo].[LOTE_EMBARQUE] " +
                            " SET [EMBARQUE_ID] =  " + EmbarqueID +
                            " WHERE LOTE_EMBARQUE_ID= " + _loteID;
                        cmd = new SqlCommand(consulta, cn.SC);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    cn.Desconectar();
                }
                catch (Exception Ex)
                {
                    cn.Desconectar();
                    MessageBox.Show(Ex.Message, "Error");
                }
            }
            else
                MessageBox.Show("Sin lotes asignados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ActualizaLote(string EmbarqueID, string _loteID)
        {
          
                ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "";
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                cn.ConectarSQLServer();
                consulta = "UPDATE [dbo].[LOTE_EMBARQUE] " +
                    " SET [EMBARQUE_ID] =  " + EmbarqueID +
                    " WHERE LOTE_EMBARQUE_ID= " + _loteID;
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception Ex)
            {
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error");
            }            
        }
    }
}
