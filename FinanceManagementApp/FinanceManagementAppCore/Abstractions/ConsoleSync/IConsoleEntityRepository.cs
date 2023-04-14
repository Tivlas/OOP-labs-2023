﻿using Domain.Entities.Interfaces;

namespace Domain.Abstractions.ConsoleSync;
public interface IConsoleEntityRepository<T> where T : IEntity
{
    bool Exists(Func<T, bool> filter);

    IReadOnlyList<T> ListAll();

    IReadOnlyList<T> List(Func<T, bool> filter);

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    T FirstOrDefault(Func<T, bool> filter);
}
