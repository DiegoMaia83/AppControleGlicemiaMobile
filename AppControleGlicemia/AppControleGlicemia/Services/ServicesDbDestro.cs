using AppControleGlicemia.Models;
using SQLite;
using System;
using System.Collections.Generic;

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
                if (destro.DataAferido > DateTime.Now)
                    throw new Exception("Não é possível inserir uma data futura");

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

        public void Alterar(ModelDestro destro)
        {
            try
            {
                int result = conn.Update(destro);

                if (result > 0)
                {
                    this.StatusMessage = string.Format("{0} registro(s) alterado(s)", result);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registro(s) alterado(s)");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModelDestro> Listar(int periodo)
        {
            //List<ModelDestro> lista = new List<ModelDestro>();

            /*
             * Período:
             * 1 - Hoje
             * 2 - Ontem
             * 3 - Últimos 5 dias
             * 4 - Últimos 30 dias
             */

            DateTime dayStart = DateTime.Now.Date;
            DateTime dayEnd = DateTime.Now.Date.AddDays(+1);

            switch (periodo)
            {
                case 0:
                    dayStart = DateTime.Now.Date;
                    dayEnd = DateTime.Now.Date.AddDays(+1);
                    break;
                case 1:
                    dayStart = DateTime.Now.Date.AddDays(-1);
                    dayEnd = DateTime.Now.Date;
                    break;
                case 2:
                    dayStart = DateTime.Now.Date.AddDays(-4);
                    dayEnd = DateTime.Now.Date.AddDays(+1);
                    break;
                case 3:
                    dayStart = DateTime.Now.Date.AddDays(-29);
                    dayEnd = DateTime.Now.Date.AddDays(+1);
                    break;
            }

            try
            {
                var resp = conn.Table<ModelDestro>().Where(x => x.DataAferido >= dayStart && x.DataAferido < dayEnd).ToList();

                List<ModelDestro> lista = new List<ModelDestro>();

                var i = 0;

                foreach (var item in resp)
                {
                    var destro = new ModelDestro();
                    destro = item;
                    destro.Stats = destro.ValorAferido > i ? "IconUp" : destro.ValorAferido < i ? "IconDown" : "IconEqual";
                    
                    if (String.IsNullOrEmpty(destro.InsulinaTipo))
                    {
                        destro.MostraInsulina = false;
                    }
                    else
                    {
                        destro.MostraInsulina = true;
                    }
                    

                    i = item.ValorAferido;

                    lista.Add(destro);
                }

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

        public ModelEstatistica RetornarMediaDia(DateTime data)
        {
            try
            {
                var dayStart = data.Date;
                var dayEnd = data.Date.AddDays(+1);

                var lista = conn.Table<ModelDestro>().Where(x => x.DataAferido >= dayStart && x.DataAferido < dayEnd).ToList();

                int quantidade = 0;
                int soma = 0;
                foreach (var item in lista){
                    soma += item.ValorAferido;
                    quantidade++;
                }

                var estatistica = new ModelEstatistica()
                {
                    Media = quantidade > 0 ? soma / quantidade : 0,
                    Quantidade = quantidade
                };

                return estatistica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModelDestro>().Delete(x => x.DestroId == id);

                StatusMessage = string.Format("{0} Registro(s) excluídos(s)", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
