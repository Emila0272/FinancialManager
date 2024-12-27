using FinancialManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace FinancialManager.Storage
{
    public class StorageService
    {
        private string path;
        public StorageService() 
        {
            path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\Transaction.json"));
        }

        public void AddTransaction(TransactionModel transaction)
        {
            var transactions = GetTransactions();

            transaction.Id = transactions.Count > 0 ? transactions.Max(t => t.Id) + 1 : 1; 
            transactions.Add(transaction);

            var json = JsonConvert.SerializeObject(transactions);

            File.WriteAllText(path, json);
        }

        public List<TransactionModel> GetTransactions()
        {
            if (!File.Exists(path))
            {
                return new List<TransactionModel>();
            }

            var json = File.ReadAllText(path);

            var transactions = JsonConvert.DeserializeObject<List<TransactionModel>>(json);

            return transactions ?? new List<TransactionModel>();
        }
    }
}
