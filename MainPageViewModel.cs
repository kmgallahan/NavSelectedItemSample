using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace NavigationViewItemIsSelectedValue.ViewModel;

public class MainPageViewModel : INotifyPropertyChanged
{
    private ObservableCollection<NavigationOption> navigationOptions = new();

    private object? selectedNavigationOption;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<NavigationOption> NavigationOptions
    {
        get => navigationOptions;
        set
        {
            if (navigationOptions != value)
            {
                navigationOptions = value;
                OnPropertyChanged();
            }
        }
    }

    public object? SelectedNavigationOption
    {
        get => selectedNavigationOption;
        set
        {
            if (selectedNavigationOption != value)
            {
                selectedNavigationOption = value;
                OnPropertyChanged();
            }
        }
    }

    public async Task LoadAsync()
    {
        IReadOnlyCollection<NavigationOption> allowedNavigationOptions = await GetAllowedNavigationOptionsAsync();

        NavigationOptions = new ObservableCollection<NavigationOption>(allowedNavigationOptions);

        SelectedNavigationOption = NavigationOptions.First();
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async Task<IReadOnlyCollection<NavigationOption>> GetAllowedNavigationOptionsAsync()
    {
        // Delay of 5 seconds to simulate a network request.
        await Task.Delay(TimeSpan.FromSeconds(5));

        return new NavigationOption[]
        {
            new() { Title = "Option 1" },
            new() { Title = "Option 2" },
            new() { Title = "Option 3" },
        };
    }
}
