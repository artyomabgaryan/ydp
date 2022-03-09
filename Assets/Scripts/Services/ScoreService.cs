public class ScoreService : IScoreService {
	private IStorageService _storageService;

	public ScoreService(IStorageService storageService) {
		_storageService = storageService;
	}

	public int GetHighScore() {
		return _storageService.GetStoredData(ScoreType.HighScore.ToString());
	}

	public int GetCurrentScore() {
		return _storageService.GetStoredData(ScoreType.CurrentScore.ToString());
	}

	public void SetCurrentScore(int score) {
		_storageService.StoreData(ScoreType.CurrentScore.ToString(), score);

		if (_storageService.GetStoredData(ScoreType.HighScore.ToString()) < score) {
			_storageService.StoreData(ScoreType.HighScore.ToString(), score);
		}
	}
}