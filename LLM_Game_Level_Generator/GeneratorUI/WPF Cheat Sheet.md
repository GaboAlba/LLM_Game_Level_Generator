Here’s a WPF Data Binding Cheat Sheet for your scenario and beyond. It covers when to use ObservableCollection, INotifyPropertyChanged, and how bindings work.

✅ 1. Binding Basics

DataContext: The object the UI binds to.

Binding Path: Property name on the DataContext.

Example:

<TextBlock Text="{Binding Name}" />


If DataContext = Person { Name = "Alice" } → Displays Alice.

✅ 2. Common Binding Modes

OneWay: UI updates when data changes, but not vice versa.

TwoWay: UI ↔ Data (use for input controls like TextBox).

OneTime: Only reads the value once (no updates later).

Example:

<TextBox Text="{Binding Username, Mode=TwoWay}" />

✅ 3. When to use ObservableCollection<T>

Use ObservableCollection<T> when:

You need UI to update when you add/remove items in a collection.

WPF listens for INotifyCollectionChanged inside ObservableCollection.

Example:

public ObservableCollection<Person> People { get; set; }
People.Add(new Person { Name = "Bob" }); // UI updates automatically

✅ 4. When to use INotifyPropertyChanged

Use when:

You want UI to update when a property value changes on your object.

You change the reference of a collection or object bound in XAML.

Example:

public class Person : INotifyPropertyChanged
{
    private string name;
    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(nameof(Name)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

✅ 5. Binding Collections

XAML:

<ListView ItemsSource="{Binding People}">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="Name" DisplayMemberBinding="{Binding Name}" />
        </GridView>
    </ListView.View>
</ListView>


Code:

public ObservableCollection<Person> People { get; set; }

public MainWindow()
{
    InitializeComponent();
    People = new ObservableCollection<Person>
    {
        new Person { Name = "Alice" },
        new Person { Name = "Bob" }
    };
    DataContext = this;
}

✅ 6. Replacing the Collection

If you replace People with a new ObservableCollection, you must:

Implement INotifyPropertyChanged on the parent object, OR

Reassign ItemsSource manually.

Why? WPF only listens to the old collection instance unless it knows the property changed.

✅ 7. Binding to Nested Properties

If you have nested objects:

<TextBlock Text="{Binding Address.City}" />


Works as long as:

Address implements INotifyPropertyChanged (if it can change).

Or Address is immutable.

✅ 8. Common Pitfalls

❌ Forgetting to set DataContext.
❌ Replacing a collection without notifying the UI.
❌ Using List<T> instead of ObservableCollection<T> for UI collections.
❌ Binding errors are silent — check Output window in Visual Studio.

✅ 9. Quick Decision Table
Scenario	What to Use
Items in a list can be added/removed	ObservableCollection<T>
Property values in an item can change	INotifyPropertyChanged
You swap the whole collection	INotifyPropertyChanged on the property OR reassign ItemsSource
One-time display (never changes)	Plain class + OneTime binding
✅ 10. Debugging Binding Issues

Enable binding errors in Output Window.

Use PresentationTraceSources.TraceLevel=High for detailed logs:

<TextBlock Text="{Binding Name, PresentationTraceSources.TraceLevel=High}" />