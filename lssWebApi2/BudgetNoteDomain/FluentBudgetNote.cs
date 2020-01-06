using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.BudgetNoteDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.BudgetNoteDomain
{

public class FluentBudgetNote :IFluentBudgetNote
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentBudgetNote() { }
        public IFluentBudgetNoteQuery Query()
        {
            return new FluentBudgetNoteQuery(unitOfWork) as IFluentBudgetNoteQuery;
        }
        public IFluentBudgetNote Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentBudgetNote;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentBudgetNote AddBudgetNotes(List<BudgetNote> newObjects)
        {
            unitOfWork.budgetNoteRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudgetNote;
        }
        public IFluentBudgetNote UpdateBudgetNotes(IList<BudgetNote> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.budgetNoteRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudgetNote;
        }
        public IFluentBudgetNote AddBudgetNote(BudgetNote newObject) {
            unitOfWork.budgetNoteRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBudgetNote;
        }
        public IFluentBudgetNote UpdateBudgetNote(BudgetNote updateObject) {
            unitOfWork.budgetNoteRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBudgetNote;

        }
        public IFluentBudgetNote DeleteBudgetNote(BudgetNote deleteObject) {
            unitOfWork.budgetNoteRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudgetNote;
        }
   	public IFluentBudgetNote DeleteBudgetNotes(List<BudgetNote> deleteObjects)
        {
            unitOfWork.budgetNoteRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBudgetNote;
        }
    }
}
