﻿using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;
using CarRentX.Repository.Concrete;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRentX.UnitOfWork.Concrete
{
	// TContext tipinde bir DbContext'e bağlı olarak çalışan bir UnitOfWork sınıfı
	public class UnitOfWork : IUnitOfWork
	{
		private readonly RentCarXEfDbContext _context;
		private IDbContextTransaction _transaction;
		private CarReadRepository _carReadRepository;
		private CarWriteRepository _carWriteRepository;
		private ColorReadRepository _colorReadRepository;
		private ColorWriteRepository _colorWriteRepository;
		private BrandReadRepository _brandReadRepository;
		private BrandWriteRepository _brandWriteRepository;
		private RentalReadRepository _rentalReadRepository;
		private RentalWriteRepository _rentalWriteRepository;
		private CustomerReadRepository _customerReadRepository;
		private CustomerWriteRepository _customerWriteRepository;
		private UserRefreshTokenReadRepository _userRefreshTokenReadRepository;
		private UserRefreshTokenWriteRepository _userRefreshTokenWriteRepository;
		public UnitOfWork(RentCarXEfDbContext context)
		{
			_context = context;
		}

		// CarReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICarReadRepository CarReadRepository => _carReadRepository = _carReadRepository ?? new CarReadRepository(_context);

		// CarWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICarWriteRepository CarWriteRepository => _carWriteRepository = _carWriteRepository ?? new CarWriteRepository(_context);

		// ColorReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public IColorReadRepository ColorReadRepository => _colorReadRepository = _colorReadRepository ?? new ColorReadRepository(_context);

		// ColorWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public IColorWriteRepository ColorWriteRepository => _colorWriteRepository = _colorWriteRepository ?? new ColorWriteRepository(_context);

		// BrandReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public IBrandReadRepository BrandReadRepository => _brandReadRepository = _brandReadRepository ?? new BrandReadRepository(_context);

		// BrandWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public IBrandWriteRepository BrandWriteRepository => _brandWriteRepository = _brandWriteRepository ?? new BrandWriteRepository(_context);

		// RentalReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public IRentalReadRepository RentalReadRepository => _rentalReadRepository = _rentalReadRepository ?? new RentalReadRepository(_context);

		// RentalWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public IRentalWriteRepository RentalWriteRepository => _rentalWriteRepository = _rentalWriteRepository ?? new RentalWriteRepository(_context);
		// CustomerReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICustomerReadRepository CustomerReadRepository => _customerReadRepository = _customerReadRepository ?? new CustomerReadRepository(_context);

		// CustomerWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICustomerWriteRepository CustomerWriteRepository => _customerWriteRepository = _customerWriteRepository ?? new CustomerWriteRepository(_context);

		// CustomerReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public IUserRefreshTokenReadRepository UserRefreshTokenReadRepository => _userRefreshTokenReadRepository = _userRefreshTokenReadRepository ?? new UserRefreshTokenReadRepository(_context);

		// CustomerWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public IUserRefreshTokenWriteRepository UserRefreshTokenWriteRepository => _userRefreshTokenWriteRepository = _userRefreshTokenWriteRepository ?? new UserRefreshTokenWriteRepository(_context);

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
		public async Task<int> CommitAsync()
		{
			var result = await _context.SaveChangesAsync(); // Yapılan değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
			if (_transaction != null)
			{
				await _transaction.CommitAsync(); // Transaction'ı commit eder (Non-Asenkron)
				_transaction.Dispose();
			}
			return result;
		}

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
		public int Commit()
		{
			var result = _context.SaveChanges(); // Yapılan değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
			if (_transaction != null)
			{
				_transaction.Commit(); // Transaction'ı commit eder (Non-Asenkron)
				_transaction.Dispose();
			}
			return result;
		}

		// Yeni bir transaction başlatır (Asenkron)
		public async Task BeginTransactionAsync()
		{
			if (_transaction != null)
			{
				_transaction.Dispose();
			}
			_transaction = await _context.Database.BeginTransactionAsync(); // Yeni bir transaction başlatır (Asenkron)
		}

		// Yeni bir transaction başlatır (Non-Asenkron)
		public void BeginTransaction()
		{
			if (_transaction != null)
			{
				_transaction.Dispose();
			}
			_transaction = _context.Database.BeginTransaction(); // Yeni bir transaction başlatır (Non-Asenkron)
		}

		// Transaction'ı rollback eder (Asenkron)
		public async Task RollbackAsync()
		{
			if (_transaction != null)
			{
				_transaction?.Dispose();
			}
			await _transaction.RollbackAsync(); // Transaction'ı rollback eder (Asenkron)
		}

		// Transaction'ı rollback eder (Non-Asenkron)
		public void Rollback()
		{
			if (_transaction != null)
			{
				_transaction.Dispose();
			}
			_transaction?.Rollback(); // Transaction'ı rollback eder (Non-Asenkron)
		}
		// Kullanılan kaynakları serbest bırakır
		public void Dispose()
		{
			_transaction?.Dispose(); // Transaction'ı dispose eder
			_context.Dispose(); // Context'i dispose eder
		}
	}
}
