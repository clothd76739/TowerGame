# 攻塔小兵
## 簡介

「攻塔小兵」為一款運用AR擴增實境技術，藉由掃描空間中的平面生成敵方高塔與玩家點擊螢幕生成我方士兵，進而進行戰鬥並以取得最高分為目標的遊戲。 


## 背景說明

AR擴增實境技術（以下簡稱AR）是一藉由使用攝影機偵測現實空間，讓螢幕中的現實環境與虛擬物件同時出現，將「現實環境影像」及「電腦虛擬影像」互相結合的一種技術，能夠實現現實與虛擬融合的景象，同時亦可結合互動要素，達到更加直觀且具趣味的體驗方式。

相比VR虛擬實境對於設備及體驗環境的高要求，只需要一台行動裝置便能體驗的AR技術能更加靈活地運用在活動及展演之中，因此我們選擇使用AR來製作這次的專案。 我們希望製作一款在使用AR的前提下運行的遊戲，探討關於AR技術與遊戲運用的更多可能性。


## 遊戲內容

本遊戲背景設定在假想的戰場上，分屬不同陣營的士兵們正為奪取更大片的領地爭鬥著。

遊戲程式啟動後即會啟動AR偵測功能，偵測到平面後生成敵方高塔，此時玩家可藉由點即螢幕生成我軍小兵對塔進行攻擊，直到將塔打倒使遊戲結束。

遊戲以擊破敵方高塔時時傷亡小兵數越少的玩家排名越高，藉由分數排行榜的機制，讓玩家們能彼此競爭，作為持續遊玩本遊戲的誘因。

在現實進行攻塔行動！
隨時隨地展開一場攻防！
在塔被擊毀前，盡可能注意我方小兵的存活。

偵測平面以建立目標塔樓，點擊屏幕生成小兵，塔和小兵會自動偵測彼此並發動攻擊。
注意小兵生成位置，別讓小兵在發動攻擊前就犧牲了！

## 各組需求

**AR程式**
* 偵測平面空間 
* 於偵測出平面上生成高塔 → 固定生成位置之Y軸
* 讀入裝置觸碰點擊 → 玩家點擊後於同一平面上生成小兵

**遊戲程式**
* 高塔旋轉 → 高塔偵測小兵方向 → 高塔攻擊小兵
* 小兵偵測高塔方向 → 小兵往高塔方向移動 → 小兵往高塔方向持續攻擊
* 小兵被高塔攻擊後消滅
* 高塔血量 → 高塔血量歸零後消失 → 遊戲結束

**UI介面**
* 遊戲開始頁面
* 排行榜頁面
* 讀取頁面
* 遊戲頁面
* 暫停頁面
* 遊戲結束計分頁面

**3D美術**
* 高塔建模
* 小兵建模

## 影片展示

https://youtu.be/_w3NBxeHyDA
