(codeMonkey教程)
核心功能
1. 建造系统
底部 UI 动态生成所有可建造建筑按钮，支持鼠标悬浮 Tooltip 提示（建筑名称 + 建造消耗资源）
鼠标左键点击空白地图放置建筑，多层校验：
拦截 UI 点击误触，点击按钮 / 面板不会造建筑
校验放置位置是否重叠、超出地图边界
校验玩家资源是否足够建造，资源不足弹出提示
全局管理选中建筑，切换按钮自动高亮当前选中项
建造成功扣除对应资源，播放放置音效
2. 怪物波次系统
自动生成怪物波次，波数越高单波怪物数量越多
多生成点随机刷新怪物，分批间隔生成怪物，避免瞬间卡顿
波次完成后进入等待倒计时，倒计时结束开启下一波
波数变更触发 UI 事件同步更新显示
3. 相机控制系统
WASD 键盘拖拽移动视角
鼠标滚轮缩放镜头大小（限制最大 / 最小视距）
鼠标边缘滚动视野（可在设置面板开关，本地配置持久化）
Cinemachine 虚拟相机管理画面
4. 设置面板 & 本地存档
音效 / 背景音乐音量增减调节，数值本地持久保存
边缘滚屏开关，使用PlayerPrefs本地存储玩家设置，重启游戏不重置
返回主菜单、暂停游戏功能（暂停冻结游戏逻辑，UI 动画不受影响）
5. 音频与提示系统
全局音效管理器，统一播放建造、按钮点击等音效
悬浮提示 Tooltip，自动计时消失，提示建造失败、资源不足等信息
项目技术栈
引擎：Unity 2D
UI：UGUI + TextMeshPro
相机：Cinemachine
版本控制：Git + GitHub
持久化：PlayerPrefs（玩家设置）
架构：单例管理器（CameraHandler、BuildingManager、ResourceManager、WaveManager、SoundManager 等）
资源管理：ScriptableObject（建筑配置表、资源配置表）


## 项目目录结构

defense/
├──
Assets/ # 游戏核心资源
│ ├── Scripts/ # 全部 C# 脚本（管理器、UI、建筑、怪物、通用工具）
│ ├── Prefabs/ # 建筑、怪物、弹窗 UI 预制体
│ ├── Sprites/ # 游戏精灵、UI 图标素材
│ ├── ScriptableObjects/ # SO 配置文件：建筑、资源、音效数据表
│ ├── Scenes/ # 游戏主场景、主菜单场景
│ └── UI/ # 界面面板、按钮模板
├── ProjectSettings/ # Unity 项目全局配置
├── Packages/ # Package Manager 包配置
├── .gitignore # Git 忽略规则，屏蔽 Library、Temp、编译缓存
└── README.md # 项目说明文档
