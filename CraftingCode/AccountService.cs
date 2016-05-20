using System;

namespace CraftingCode
{
    public class AccountService
    {
        private readonly IOutputWriter _outputWriter;
        private readonly IBankRepository _bankRepository;


        public AccountService(IOutputWriter outputWriter, IBankRepository bankRepository)
        {
            _outputWriter = outputWriter;
            _bankRepository = bankRepository;
        }

        public void Deposit(int amount)
        {
            _bankRepository.Deposit(amount);
        }

        public void Withdraw(int amount)
        {
            _bankRepository.Withdraw(amount);
        }

        public void PrintStatement()
        {
            var statements = _bankRepository.Statements();
            _outputWriter.Log("DATE | AMOUNT | BALANCE");
            foreach (var statement in statements)
            {
                var formatedStatement = string.Format("{0} | {1} | {2}", statement.Date.ToString("d"),
                    statement.ChangeAmount, statement.TotalBalance);
                _outputWriter.Log(formatedStatement);
            }
        }

    }

    public interface IBankRepository
    {
        void Deposit(int amount);
        void Withdraw(int amount);
        Statement[] Statements();
    }

    public interface IOutputWriter
    {
        void Log(string message);
    }

    public class Statement
    {
        public DateTime Date { get; set; }
        public int ChangeAmount { get; set; }
        public int TotalBalance { get; set; }
    }
}
