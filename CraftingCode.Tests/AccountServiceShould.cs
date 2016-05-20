using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace CraftingCode.Tests
{
    public class AccountServiceShould
    {
        [Fact]
        public void PrintStatementContainingTransactionsInReverseChronologicalOrder()
        {
            var console = new Mock<IOutputWriter>();

            var accountService = new AccountService(console.Object, null);

            accountService.Deposit(1000);
            accountService.Withdraw(100);
            accountService.Deposit(500);
            accountService.PrintStatement();

            console.Verify(c => c.Log("DATE | AMOUNT | BALANCE"));
            console.Verify(c => c.Log("20/05/2016 | 500.00 | 1400.00"));
            console.Verify(c => c.Log("20/05/2016 | -100.00 | 900.00"));
            console.Verify(c => c.Log("20/05/2016 | 1000.00 | 1000.00"));
            
        }

        [Theory, AutoMoqData]
        public void IncreaseTheBalanceWhenDepositIsMade(Mock<IOutputWriter> mockedOutputWriter, Mock<IBankRepository> mockedBankRepository)
        {
            var accountService = new AccountService(mockedOutputWriter.Object, mockedBankRepository.Object);
            accountService.Deposit(100);

            mockedBankRepository.Verify(br => br.Deposit(100), Times.Once);
        }
        [Theory, AutoMoqData]
        public void DecreaseTheBalaceWhenWithdrawalIsMada(Mock<IOutputWriter> mockedOutputWriter,
            Mock<IBankRepository> mockedBankRepository)
        {
            var accountService = new AccountService(mockedOutputWriter.Object, mockedBankRepository.Object);
            accountService.Withdraw(200);

            mockedBankRepository.Verify(br => br.Withdraw(200), Times.Once);
        }

        [Theory, AutoMoqData]
        public void PrintDataWhenPrintStatementIsExecuted(Mock<IOutputWriter> mockedOutputWriter,
            Mock<IBankRepository> mockedBankRepository)
        {
            var testStatements = new Statement[] { new Statement
            {
                Date = DateTime.Now,
                ChangeAmount = 100,
                TotalBalance = 1000
            } };
            var expectedOutput = string.Format("{0} | {1} | {2}", testStatements[0].Date.ToString("d"),
                testStatements[0].ChangeAmount, testStatements[0].TotalBalance);
            mockedBankRepository.Setup(br => br.Statements()).Returns(testStatements);
            var accountService = new AccountService(mockedOutputWriter.Object, mockedBankRepository.Object);

            accountService.PrintStatement();

            mockedOutputWriter.Verify(ow => ow.Log("DATE | AMOUNT | BALANCE"), Times.Once);
            
            mockedOutputWriter.Verify(ow => ow.Log(expectedOutput), Times.Once);
        }


    }
}
