using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.EGUI;
using Engine.Objects.Types;
using Engine.Objects.Types.Readed;
using UnityStandardAssets.CrossPlatformInput;

namespace Engine.Objects {

	public class ObjectReaded {
	
		private bool  readState  = false; // показывает, читается ли объект в данный момент времени или нет
		private int   page       = 0;     // показывает номер текущей страницы
		private float timeStamp  = 0f;
		
		private AudioSource      audioSource; // аудио проигрыватель
		private IReadedType      readed; // наш интерфейс читаемого объекта
		private List<ReadedPage> pages; // список страниц

		private ReadedPageGUIRenderer readedGUIRenderer;
		
		public ObjectReaded(IReadedType readed, List<ReadedPage> pages, AudioSource audioSource){
			this.readed=readed;
			this.pages=pages;
			this.audioSource=audioSource;
		}
		
		public int getCurrentPage(){
			return page;
		}
		
		private void play(AudioClip clip){
			audioSource.clip = clip;
			audioSource.Play();
		}
		
		///<summary>
		/// Перелистывает страницу вперёд
		///</summary>
		public int nextPage(){
			int oldPage = page;
			page = page>=getEndPage() ? getEndPage() : page++;
			readed.onReadChangePage(page);
			
				if(oldPage!=page)
					play(ReadedSoundData.getInstance().getChangePageSound()); // воспроисзводим звук перелистывания страницы
			
			return page;
		}
		
		///<summary>
		/// Перелистывает страницу назад
		///</summary>
		public int backPage(){
			int oldPage = page;
			page = page<=getStartPage() ? getStartPage() : page--;
			readed.onReadChangePage(page);
			
				if(oldPage!=page)
					play(ReadedSoundData.getInstance().getChangePageSound()); // воспроисзводим звук перелистывания страницы
			
			return page;
		}
		
		public int getStartPage(){
			return 0;
		}
		
		public int getEndPage(){
			return pages.Count-1;
		}
		
		
		///<summary>
		/// Начало чтения
		///</summary>
		public void onReadStart(){

			if (readedGUIRenderer==null)
				readedGUIRenderer = SingletonNames.getGUI().GetComponent<GUIController>().getReadedPageGUIRenderer();

			page=0;
			readState = true;
			timeStamp = Time.time;
			
			play(ReadedSoundData.getInstance().getOpenReadedSound()); // воспроизводим звук открытия читаемого предмета
		}
		
		///<summary>
		/// Конец чтения
		///</summary>
		public void onReadEnd(){
			page=0;
			readState = false;
			timeStamp = 0f;
			
			play(ReadedSoundData.getInstance().getCloseReadedSound()); // воспроизводим звук закрытия читаемого предмета
		}
		
		///<summary>
		/// Обновление чтения (используется в теле основного цикла)
		///</summary>
		public void update(){
			
			if(!readState) return; // выходим если текущий объект не читается
			
			if(CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.NEXTPAGE))
				nextPage(); // перелистываем страницу вперёд
			
			if(CrossPlatformInputManager.GetButtonDown(SingletonNames.Input.BACKPAGE))
				backPage(); // перелистываем страницу назад
			
			ReadedPage currentPage = pages[page]; // извлекаем текущую страницу
			readedGUIRenderer.printReadedPage(currentPage); // подаём страницу на отрисовку
			
			if(!currentPage.isReaded) return; // если страница не прочитанна нашим гг
			
			if(Time.time-timeStamp>=currentPage.getReadDelay()){ // считаем время просмотра и сравниваем
			// с минимальным временем, которое надо затратить на чтение для вызова соответствующего event-а
				currentPage.isReaded=true; // почемаем страницу как прочитанную
				readed.onPageReaded(page); // дёргаем соответствующий метод у интерфейса
			}
			
		}

	}
	
}

