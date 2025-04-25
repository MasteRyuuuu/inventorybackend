using InventorySystem.Data;
using InventorySystem.Models;

namespace InventorySystem.Services
{
    public class TransactionService
    {
        private readonly InventoryDbContext _context;

        public TransactionService(InventoryDbContext context)
        {
            _context = context;
        }

        public void ScanIn(Transaction transaction)
        {
            transaction.Operation = "in";
            transaction.CreatedAt = DateTime.UtcNow;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void ScanOut(Transaction transaction)
        {
            transaction.Operation = "out";
            transaction.CreatedAt = DateTime.UtcNow;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public bool Undo(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == transactionId);
            if (transaction == null)
                return false;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return true;
        }
    }
}
