namespace RealBankAccount
{
    public abstract class BankAccount
    {
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
    }
    public class SavingsAccount : BankAccount
    {
        public double InterestRate { get; set; }
        public override void Deposit(double amount)
        {
            Balance += amount + (amount * InterestRate);
        }
        public override void Withdraw(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new Exception("Insufficient funds");
            }
        }
    }
    public class CheckingAccount : BankAccount
    {
        public double OverdraftLimit { get; set; }
        public override void Deposit(double amount)
        {
            Balance += amount;
        }
        public override void Withdraw(double amount)
        {
            if (Balance + OverdraftLimit >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new Exception("Insufficient funds");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount savingsAccount = new SavingsAccount { AccountNumber = 1001, Balance = 1000, InterestRate = 0.01 };
            CheckingAccount checkingAccount = new CheckingAccount { AccountNumber = 2001, Balance = 2000, OverdraftLimit = 500 };
            Console.WriteLine($"Initial Savings Account Balance: {savingsAccount.Balance}");
            Console.WriteLine($"Initial Checking Account Balance: {checkingAccount.Balance}");
            savingsAccount.Deposit(500);
            checkingAccount.Deposit(1000);
            Console.WriteLine($"After Deposit - Savings Account Balance: {savingsAccount.Balance}");
            Console.WriteLine($"After Deposit - Checking Account Balance: {checkingAccount.Balance}");
            try
            {
                savingsAccount.Withdraw(2000);
                Console.WriteLine($"After Withdrawal - Savings Account Balance: {savingsAccount.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Savings Account Withdrawal Failed - {ex.Message}");
            }
            try
            {
                checkingAccount.Withdraw(3000);
                Console.WriteLine($"After Withdrawal - Checking Account Balance: {checkingAccount.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Checking Account Withdrawal Failed - {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}