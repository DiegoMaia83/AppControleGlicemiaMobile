using AppControleGlicemia.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleGlicemia.Services
{
    public class ServicesDbDestro
    {
        SQLiteConnection conn;

        public string StatusMessage { get; set; }

        public ServicesDbDestro(string dbPath)
        {
            if (dbPath == "")
                dbPath = App.DbPath;

            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelDestro>();
        }

        public void Inserir(ModelDestro destro)
        {
            try
            {
                int result = conn.Insert(destro);

                if (result > 0)
                {
                    this.StatusMessage = string.Format("{0} registro(s) adicionado(s)", result);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registro(s) adicionado(s)");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModelDestro> Listar()
        {
            List<ModelDestro> lista = new List<ModelDestro>();

            try
            {
                lista = conn.Table<ModelDestro>().ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ModelDestro Retornar(int id)
        {
            try
            {
                return conn.Table<ModelDestro>().Where(x => x.DestroId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModelDestro RetornarUltimaAfericao()
        {
            try
            {
                return conn.Table<ModelDestro>().OrderByDescending(x => x.DataAferido).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
