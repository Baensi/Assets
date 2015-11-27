using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Engine {

	public class ConfigReader {

		public ConfigReader() {

		}

		/// <summary>
		/// ������ ������������ �� ����� � ����� (�������)
		/// </summary>
		/// <param name="configFile">������� ���� ������������</param>
		public void readConfig(string configFile) {

			if(!File.Exists(configFile)){
				createDefaultConfig(configFile);
				return;
			}

			try {

				StreamReader stream = new StreamReader(configFile);

				string line;

					while (!stream.EndOfStream) {

						line = stream.ReadLine().TrimStart();

						if (line.IndexOf("=")!=-1) {
							string[] parameter = line.Split('=');


							switch (parameter[0]) {
								case GameConfig.CONFIG_CURRENT_LANGUAGE:
									GameConfig.Localization = parameter[1];
									break;
							}

						}

					}

				stream.Close();
				stream.Dispose();
				stream = null;

			} catch (Exception e) {

				Debug.LogError("�� ������� ��������� ������������! ["+configFile+"]");
				foreach(object d in e.Data.Keys)
					Debug.LogError(d.ToString()+": "+e.Data[d].ToString());

            }

		}

		/// <summary>
		/// �������� ������������ �� ���������
		/// </summary>
		/// <param name="configFile">�������� ���� ������������</param>
		/// <param name="data">����� (�������)</param>
		public void createDefaultConfig(string configFile) {

			try {

				StreamWriter writer = new StreamWriter(configFile);

				writer.WriteLine("[Configurations]\n");

					writer.WriteLine("\t"+GameConfig.CONFIG_CURRENT_LANGUAGE+"=ru");

				writer.WriteLine("[End]\n");

				writer.Close();
				writer.Dispose();
				writer = null;

			} catch (Exception e) {

				Debug.LogError("�� ������� ������� ������������! ["+configFile+"]");
				foreach (object d in e.Data.Keys)
					Debug.LogError(d.ToString()+": "+e.Data[d].ToString());

			}

		}

	}

}