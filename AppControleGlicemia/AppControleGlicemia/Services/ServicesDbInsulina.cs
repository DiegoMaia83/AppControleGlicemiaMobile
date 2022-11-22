using AppControleGlicemia.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace AppControleGlicemia.Services
{
    public class ServicesDbInsulina
    {
        SQLiteConnection conn;

        public string StatusMessage { get; set; }

        public ServicesDbInsulina(string dbPath)
        {
            if (dbPath == "")
                dbPath = App.DbPath;

            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModeInsulina>();
        }

        public void Inserir(ModeInsulina insulina)
        {
            try
            {
                int result = conn.Insert(insulina);

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

        public List<ModeInsulina> Listar()
        {
            try
            {
                var lista = conn.Table<ModeInsulina>().ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModeInsulina>().Delete(x => x.InsulinaId == id);

                StatusMessage = string.Format("{0} Registro(s) excluídos(s)", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
