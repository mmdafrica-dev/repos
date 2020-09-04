import { Injectable } from '@angular/core';
import { CacheFactory } from 'cachefactory';
const cacheFactory = new CacheFactory();
let cache;
let cacheGenny;
let cacheGenricFile;
@Injectable()
export class StorageService {
  public FilterData: any;
  cacheGenricFileKey = 'cacheGenricFileKey';
  constructor() {
    if (!cacheFactory.exists('mmdCache')) {
      cache = cacheFactory.createCache('mmdCache', {
        deleteOnExpire: 'aggressive',
        storageMode: 'localStorage'
      });
    }
    this.createGennyCache();
    this.createGenricFileCache();

  }


  createGennyCache() {
    if (!cacheFactory.exists('mmdCacheGenny')) {
      cacheGenny = cacheFactory.createCache('mmdCacheGenny', {
        deleteOnExpire: 'aggressive',
        storageMode: 'localStorage'
      });
    }
  }

  createGenricFileCache() {
    if (!cacheFactory.exists('mmdcacheGenricFile')) {
      cacheGenricFile = cacheFactory.createCache('mmdcacheGenricFile', {
        deleteOnExpire: 'aggressive',
        storageMode: 'localStorage'
      });
    }
  }


  //  maxAge: 15 * 60 * 1000,
  setObject(key: string, value: {}) {
    cache.put(key, value);
  }

  setGennyObject(key: string, value: {}) {
    cacheGenny.put(key, value);
  }

  getGennyObject(key: string) {
    return cacheGenny.get(key);
  }

  setValue(key: string, value: string) {
    cache.put(key, value);
  }

  getObject(key: string) {
    return cache.get(key);
  }

  getValue(key: string) {
    return cache.get(key);
  }

  removeItem(key: string) {
    return cache.remove(key);
  }

  removeGenny() {
    cacheFactory.destroy('mmdCacheGenny');
    this.createGennyCache();
  }



  setGenericData(data) {
    cacheGenricFile.put(this.cacheGenricFileKey, data);
  }

  getGenericData() {
    return cacheGenricFile.get(this.cacheGenricFileKey);
  }

  removeGenericData() {
    cacheFactory.destroy('mmdcacheGenricFile');
    this.createGenricFileCache();
  }
}
