using System;
using System.Collections.Generic;
using System.Linq;

public class StatisticsPanelUIController : BaseMainMenuPanelUIController
{
    protected override MainMenuState State => MainMenuState.Statistics;
    
    readonly StatisticsPanelUIView _view;
    readonly IMiniGameStatisticsModel _miniGameStatisticsModel;
    readonly PoolableViewFactory _viewFactory;
    readonly List<StatisticsEntryUIView> _entryUIViews = new();
    
    public StatisticsPanelUIController (
        IMainMenuModel model,
        StatisticsPanelUIView view,
        IMiniGameStatisticsModel miniGameStatisticsModel,
        PoolableViewFactory viewFactory
    ) : base(model)
    {
        _view = view;
        _miniGameStatisticsModel = miniGameStatisticsModel;
        _viewFactory = viewFactory;
    }
    
    public override void Initialize ()
    {
        base.Initialize();
        AddViewListeners();
        
        _viewFactory.SetupPool(_view.StatisticsEntryPrefab);
    }
    
    protected override void Enable ()
    {
        _view.SetActive(true);
        
        List<MiniGameType> miniGamesList = ((MiniGameType[])Enum.GetValues(typeof(MiniGameType))).ToList();
        miniGamesList.Remove(MiniGameType.None);
        
        CreateMissingInstances(miniGamesList);
        UpdateInstances(miniGamesList);
    }

    protected override void Disable ()
    {
        _view.SetActive(false);
    }
    
    void CreateMissingInstances (List<MiniGameType> miniGamesList)
    {
        int activeCount = _entryUIViews.Count;
        int missingCount = miniGamesList.Count - activeCount;
        for (int i = activeCount; i < missingCount; i++)
        {
            StatisticsEntryUIView entryUIView = _viewFactory.GetView<StatisticsEntryUIView>(_view.EntriesContainer);
            _entryUIViews.Add(entryUIView);
        }
    }

    void UpdateInstances (List<MiniGameType> miniGamesList)
    {
        for (int i = 0; i < miniGamesList.Count; i++)
        {
            MiniGameStatistics statistics = _miniGameStatisticsModel.GetMiniGameStatisticsByType(miniGamesList[i]);
            StatisticsEntryUIView victoryEntryUIView = _entryUIViews[i];
            
            victoryEntryUIView.SetNameText(statistics.Name);
            victoryEntryUIView.SetVictoryCountText(statistics.VictoryCount.ToString());
            victoryEntryUIView.SetDefeatCountText(statistics.DefeatCount.ToString());
        }
    }

    void AddViewListeners ()
    {
        _view.OnBackButtonClick += HandleBackButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnBackButtonClick -= HandleBackButtonClick;
    }

    void HandleBackButtonClick ()
    {
        Model.ChangeMainMenuState(MainMenuState.Menu);
    }
    
    public override void Dispose ()
    {
        _entryUIViews.DisposeAndClear();
        
        base.Dispose();
        RemoveViewListeners();
    }
}