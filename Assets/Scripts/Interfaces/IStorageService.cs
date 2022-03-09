public interface IStorageService {
	public int GetStoredData(string key);
	public void StoreData(string key, int value);
}
