﻿// BudgetFirst 
// ©2016 Thomas Mühlgrabner
//
// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
//
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================
namespace BudgetFirst.Budget.Domain.Commands.Account
{
    using BudgetFirst.Budget.Repositories;
    using BudgetFirst.SharedInterfaces.Commands;
    using BudgetFirst.SharedInterfaces.Messaging;

    /// <summary>
    /// Handles commands related to Accounts
    /// </summary>
    public class AccountCommandHandler : ICommandHandler<CreateAccountCommand>, ICommandHandler<ChangeAccountNameCommand>
    {
        /// <summary>
        /// The Account repository
        /// </summary>
        private readonly AccountRepository repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="AccountCommandHandler"/> class.
        /// </summary>
        /// <param name="repository">The account repository</param>
        public AccountCommandHandler(AccountRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handles the ChangeAccountName command
        /// </summary>
        /// <param name="command">The ChangeAccountNameCommand</param>
        /// <param name="aggregateUnitOfWork">The event transaction</param>
        public void Handle(ChangeAccountNameCommand command, IAggregateUnitOfWork aggregateUnitOfWork)
        {
            Aggregates.Account account = this.repository.Find(command.Id, aggregateUnitOfWork);
            account.ChangeName(command.Name);
            this.repository.Save(account, aggregateUnitOfWork);
        }

        /// <summary>
        /// Handles the CreateAccountName command
        /// </summary>
        /// <param name="command">The CreateAccountNameCommand</param>
        /// <param name="aggregateUnitOfWork">The event transaction</param>
        public void Handle(CreateAccountCommand command, IAggregateUnitOfWork aggregateUnitOfWork)
        {
            Aggregates.Account account = new Aggregates.Account(command.Id, command.Name);
            this.repository.Save(account, aggregateUnitOfWork);
        }
    }
}
