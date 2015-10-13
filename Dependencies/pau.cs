// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from beacukai on 2011-02-12 16:16:58Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
using System;
using System.ComponentModel;
using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
using System.Diagnostics;


public partial class BeAcUkAi : DataContext
{
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
	
	
	public BeAcUkAi(string connectionString) : 
			base(connectionString)
	{
		this.OnCreated();
	}
	
	public BeAcUkAi(string connection, MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		this.OnCreated();
	}
	
	public BeAcUkAi(IDbConnection connection, MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		this.OnCreated();
	}
	
	public Table<NationalityAttention> NationalityAttention
	{
		get
		{
			return this.GetTable<NationalityAttention>();
		}
	}
	
	public Table<PaUDpO> PaUDpO
	{
		get
		{
			return this.GetTable<PaUDpO>();
		}
	}
	
	public Table<PaUPassenger> PaUPassenger
	{
		get
		{
			return this.GetTable<PaUPassenger>();
		}
	}
}

#region Start MONO_STRICT
#if MONO_STRICT

public partial class BeAcUkAi
{
	
	public BeAcUkAi(IDbConnection connection) : 
			base(connection)
	{
		this.OnCreated();
	}
}
#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT

public partial class BeAcUkAi
{
	
	public BeAcUkAi(IDbConnection connection) : 
			base(connection, new DbLinq.MySql.MySqlVendor())
	{
		this.OnCreated();
	}
	
	public BeAcUkAi(IDbConnection connection, IVendor sqlDialect) : 
			base(connection, sqlDialect)
	{
		this.OnCreated();
	}
	
	public BeAcUkAi(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
			base(connection, mappingSource, sqlDialect)
	{
		this.OnCreated();
	}
}
#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
#endregion

[Table(Name="beacukai.nationality_attention")]
public partial class NationalityAttention : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private long _id;
	
	private string _nationality;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(long value);
		
		partial void OnNationalityChanged();
		
		partial void OnNationalityChanging(string value);
		#endregion
	
	
	public NationalityAttention()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_id", Name="ID", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public long ID
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((_id != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._id = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_nationality", Name="Nationality", DbType="varchar(200)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Nationality
	{
		get
		{
			return this._nationality;
		}
		set
		{
			if (((_nationality == value) 
						== false))
			{
				this.OnNationalityChanging(value);
				this.SendPropertyChanging();
				this._nationality = value;
				this.SendPropertyChanged("Nationality");
				this.OnNationalityChanged();
			}
		}
	}
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="beacukai.pau_dpo")]
public partial class PaUDpO : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private string _firstName;
	
	private long _id;
	
	private string _lastName;
	
	private string _name;
	
	private string _notes;
	
	private string _passport;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnFirstNameChanged();
		
		partial void OnFirstNameChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(long value);
		
		partial void OnLastNameChanged();
		
		partial void OnLastNameChanging(string value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnNotesChanged();
		
		partial void OnNotesChanging(string value);
		
		partial void OnPassportChanged();
		
		partial void OnPassportChanging(string value);
		#endregion
	
	
	public PaUDpO()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_firstName", Name="first_name", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string FirstName
	{
		get
		{
			return this._firstName;
		}
		set
		{
			if (((_firstName == value) 
						== false))
			{
				this.OnFirstNameChanging(value);
				this.SendPropertyChanging();
				this._firstName = value;
				this.SendPropertyChanged("FirstName");
				this.OnFirstNameChanged();
			}
		}
	}
	
	[Column(Storage="_id", Name="id", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public long ID
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((_id != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._id = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_lastName", Name="last_name", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string LastName
	{
		get
		{
			return this._lastName;
		}
		set
		{
			if (((_lastName == value) 
						== false))
			{
				this.OnLastNameChanging(value);
				this.SendPropertyChanging();
				this._lastName = value;
				this.SendPropertyChanged("LastName");
				this.OnLastNameChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_notes", Name="notes", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Notes
	{
		get
		{
			return this._notes;
		}
		set
		{
			if (((_notes == value) 
						== false))
			{
				this.OnNotesChanging(value);
				this.SendPropertyChanging();
				this._notes = value;
				this.SendPropertyChanged("Notes");
				this.OnNotesChanged();
			}
		}
	}
	
	[Column(Storage="_passport", Name="passport", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Passport
	{
		get
		{
			return this._passport;
		}
		set
		{
			if (((_passport == value) 
						== false))
			{
				this.OnPassportChanging(value);
				this.SendPropertyChanging();
				this._passport = value;
				this.SendPropertyChanged("Passport");
				this.OnPassportChanged();
			}
		}
	}
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="beacukai.pau_passenger")]
public partial class PaUPassenger : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private System.Nullable<System.DateTime> _birthDate;
	
	private string _cdSt;
	
	private System.Nullable<System.DateTime> _date;
	
	private string _fareClass;
	
	private string _firstName;
	
	private System.Nullable<System.DateTime> _flightDate;
	
	private string _flightNo;
	
	private string _gender;
	
	private long _id;
	
	private string _lastName;
	
	private string _name;
	
	private string _nationality;
	
	private string _notes;
	
	private string _passport;
	
	private string _picture;
	
	private string _pnr;
	
	private string _seatNo;
	
	private string _seqnO;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBirthDateChanged();
		
		partial void OnBirthDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnCDstChanged();
		
		partial void OnCDstChanging(string value);
		
		partial void OnDateChanged();
		
		partial void OnDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnFareClassChanged();
		
		partial void OnFareClassChanging(string value);
		
		partial void OnFirstNameChanged();
		
		partial void OnFirstNameChanging(string value);
		
		partial void OnFlightDateChanged();
		
		partial void OnFlightDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnFlightNoChanged();
		
		partial void OnFlightNoChanging(string value);
		
		partial void OnGenderChanged();
		
		partial void OnGenderChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(long value);
		
		partial void OnLastNameChanged();
		
		partial void OnLastNameChanging(string value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnNationalityChanged();
		
		partial void OnNationalityChanging(string value);
		
		partial void OnNotesChanged();
		
		partial void OnNotesChanging(string value);
		
		partial void OnPassportChanged();
		
		partial void OnPassportChanging(string value);
		
		partial void OnPictureChanged();
		
		partial void OnPictureChanging(string value);
		
		partial void OnPNRChanged();
		
		partial void OnPNRChanging(string value);
		
		partial void OnSeatNoChanged();
		
		partial void OnSeatNoChanging(string value);
		
		partial void OnSEQNoChanged();
		
		partial void OnSEQNoChanging(string value);
		#endregion
	
	
	public PaUPassenger()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_birthDate", Name="birth_date", DbType="date", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<System.DateTime> BirthDate
	{
		get
		{
			return this._birthDate;
		}
		set
		{
			if ((_birthDate != value))
			{
				this.OnBirthDateChanging(value);
				this.SendPropertyChanging();
				this._birthDate = value;
				this.SendPropertyChanged("BirthDate");
				this.OnBirthDateChanged();
			}
		}
	}
	
	[Column(Storage="_cdSt", Name="cdst", DbType="varchar(50)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string CDst
	{
		get
		{
			return this._cdSt;
		}
		set
		{
			if (((_cdSt == value) 
						== false))
			{
				this.OnCDstChanging(value);
				this.SendPropertyChanging();
				this._cdSt = value;
				this.SendPropertyChanged("CDst");
				this.OnCDstChanged();
			}
		}
	}
	
	[Column(Storage="_date", Name="date", DbType="date", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<System.DateTime> Date
	{
		get
		{
			return this._date;
		}
		set
		{
			if ((_date != value))
			{
				this.OnDateChanging(value);
				this.SendPropertyChanging();
				this._date = value;
				this.SendPropertyChanged("Date");
				this.OnDateChanged();
			}
		}
	}
	
	[Column(Storage="_fareClass", Name="fare_class", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string FareClass
	{
		get
		{
			return this._fareClass;
		}
		set
		{
			if (((_fareClass == value) 
						== false))
			{
				this.OnFareClassChanging(value);
				this.SendPropertyChanging();
				this._fareClass = value;
				this.SendPropertyChanged("FareClass");
				this.OnFareClassChanged();
			}
		}
	}
	
	[Column(Storage="_firstName", Name="first_name", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string FirstName
	{
		get
		{
			return this._firstName;
		}
		set
		{
			if (((_firstName == value) 
						== false))
			{
				this.OnFirstNameChanging(value);
				this.SendPropertyChanging();
				this._firstName = value;
				this.SendPropertyChanged("FirstName");
				this.OnFirstNameChanged();
			}
		}
	}
	
	[Column(Storage="_flightDate", Name="flight_date", DbType="date", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<System.DateTime> FlightDate
	{
		get
		{
			return this._flightDate;
		}
		set
		{
			if ((_flightDate != value))
			{
				this.OnFlightDateChanging(value);
				this.SendPropertyChanging();
				this._flightDate = value;
				this.SendPropertyChanged("FlightDate");
				this.OnFlightDateChanged();
			}
		}
	}
	
	[Column(Storage="_flightNo", Name="flight_no", DbType="varchar(50)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string FlightNo
	{
		get
		{
			return this._flightNo;
		}
		set
		{
			if (((_flightNo == value) 
						== false))
			{
				this.OnFlightNoChanging(value);
				this.SendPropertyChanging();
				this._flightNo = value;
				this.SendPropertyChanged("FlightNo");
				this.OnFlightNoChanged();
			}
		}
	}
	
	[Column(Storage="_gender", Name="gender", DbType="varchar(50)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Gender
	{
		get
		{
			return this._gender;
		}
		set
		{
			if (((_gender == value) 
						== false))
			{
				this.OnGenderChanging(value);
				this.SendPropertyChanging();
				this._gender = value;
				this.SendPropertyChanged("Gender");
				this.OnGenderChanged();
			}
		}
	}
	
	[Column(Storage="_id", Name="id", DbType="bigint(20)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public long ID
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((_id != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._id = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_lastName", Name="last_name", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string LastName
	{
		get
		{
			return this._lastName;
		}
		set
		{
			if (((_lastName == value) 
						== false))
			{
				this.OnLastNameChanging(value);
				this.SendPropertyChanging();
				this._lastName = value;
				this.SendPropertyChanged("LastName");
				this.OnLastNameChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_nationality", Name="nationality", DbType="varchar(50)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Nationality
	{
		get
		{
			return this._nationality;
		}
		set
		{
			if (((_nationality == value) 
						== false))
			{
				this.OnNationalityChanging(value);
				this.SendPropertyChanging();
				this._nationality = value;
				this.SendPropertyChanged("Nationality");
				this.OnNationalityChanged();
			}
		}
	}
	
	[Column(Storage="_notes", Name="notes", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Notes
	{
		get
		{
			return this._notes;
		}
		set
		{
			if (((_notes == value) 
						== false))
			{
				this.OnNotesChanging(value);
				this.SendPropertyChanging();
				this._notes = value;
				this.SendPropertyChanged("Notes");
				this.OnNotesChanged();
			}
		}
	}
	
	[Column(Storage="_passport", Name="passport", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Passport
	{
		get
		{
			return this._passport;
		}
		set
		{
			if (((_passport == value) 
						== false))
			{
				this.OnPassportChanging(value);
				this.SendPropertyChanging();
				this._passport = value;
				this.SendPropertyChanged("Passport");
				this.OnPassportChanged();
			}
		}
	}
	
	[Column(Storage="_picture", Name="picture", DbType="varchar(500)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Picture
	{
		get
		{
			return this._picture;
		}
		set
		{
			if (((_picture == value) 
						== false))
			{
				this.OnPictureChanging(value);
				this.SendPropertyChanging();
				this._picture = value;
				this.SendPropertyChanged("Picture");
				this.OnPictureChanged();
			}
		}
	}
	
	[Column(Storage="_pnr", Name="pnr", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string PNR
	{
		get
		{
			return this._pnr;
		}
		set
		{
			if (((_pnr == value) 
						== false))
			{
				this.OnPNRChanging(value);
				this.SendPropertyChanging();
				this._pnr = value;
				this.SendPropertyChanged("PNR");
				this.OnPNRChanged();
			}
		}
	}
	
	[Column(Storage="_seatNo", Name="seat_no", DbType="varchar(100)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string SeatNo
	{
		get
		{
			return this._seatNo;
		}
		set
		{
			if (((_seatNo == value) 
						== false))
			{
				this.OnSeatNoChanging(value);
				this.SendPropertyChanging();
				this._seatNo = value;
				this.SendPropertyChanged("SeatNo");
				this.OnSeatNoChanged();
			}
		}
	}
	
	[Column(Storage="_seqnO", Name="seq_no", DbType="varchar(50)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string SEQNo
	{
		get
		{
			return this._seqnO;
		}
		set
		{
			if (((_seqnO == value) 
						== false))
			{
				this.OnSEQNoChanging(value);
				this.SendPropertyChanging();
				this._seqnO = value;
				this.SendPropertyChanged("SEQNo");
				this.OnSEQNoChanged();
			}
		}
	}
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}
