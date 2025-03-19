using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
internal class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type user_role as enum
        (
            'admin',
            'customer'
        );

        create type operation_type as enum
        (
            'Deposit',
            'Withdrawal' 
        );

        create table users
        (
            user_id bigint primary key generated always as identity ,
            user_name text not null ,
            user_role text not null ,
            password text not null
        );

        create table bank_account
        (
            user_name text not null,
            balance text not null 
        );

        create table operation
        (
            id bigint primary key generated always as identity ,
            operation_type text not null,
            amount numeric not null
            user_name text not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;
        drop table bank_account;
        drop table operation;

        drop type user_role;
        drop type operation_type;
        """;
}