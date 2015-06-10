using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Products_Management.BL
{
    class CLS_PRODUCT
    {
        public DataTable GET_ALL_CATEGORIES()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            Dt = DAL.SelectData("GET_ALL_CATEGORIES", null);
            DAL.Close();
            return Dt;
        }
        public void ADD_PROUDCT(int ID_CAT, string ID_PRODUCT, string LABEL_PRODUCT, int QTE_IN_STOCK, string PRICE, byte[] IMAGE_PRODUCT)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@ID_CAT", SqlDbType.Int);
            param[0].Value = ID_CAT;

            param[1] = new SqlParameter("@ID_PRODUCT", SqlDbType.NVarChar,50);
            param[1].Value = ID_PRODUCT;

            param[2] = new SqlParameter("@LABEL_PRODUCT", SqlDbType.NVarChar,50);
            param[2].Value = LABEL_PRODUCT;

            param[3] = new SqlParameter("@QTE_IN_STOCK", SqlDbType.Int);
            param[3].Value = QTE_IN_STOCK;

            param[4] = new SqlParameter("@PRICE", SqlDbType.NVarChar,50);
            param[4].Value = PRICE;

            param[5] = new SqlParameter("@IMAGE_PRODUCT", SqlDbType.Image);
            param[5].Value = IMAGE_PRODUCT;

            DAL.ExecuteCommand("ADD_PROUDCT", param);
            DAL.Close();
        }

        public DataTable VERIFY_PRODUCT_ID(string ID)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            DataTable Dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID_PRODUCT", SqlDbType.NVarChar, 50);
            param[0].Value = ID;
            Dt = DAL.SelectData("VERIFY_PRODUCT_ID", param);
            DAL.Close();
            return Dt;
        }
    }
}
