using UnityEngine;

public class StorageService : IStorageService {
	public int GetStoredData(string key) {
		return PlayerPrefs.GetInt(key, 0);
	}

	public void StoreData(string key, int value) {
		PlayerPrefs.SetInt(key, value);
		PlayerPrefs.Save();
	}
}