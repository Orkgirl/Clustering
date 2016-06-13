using System;
using System.Linq;
using System.Net.Sockets;
using Common.Net.Commands;
using Server.DataBase;

namespace Server.Logic
{
    public class LogicManager
    {
        public static void Process(Socket handler, CommandBase command)
        {
            Console.WriteLine("[LogicManager][Process]: " + command.CommandType);
            switch (command.CommandType)
            {
                case CommandType.GetRegion:
                    var regions = DataBaseManager.RegionJsonRecordMap.Items.ToDictionary(region => region.id_region, region => region.name_region);
                    AsynchronousSocketListener.Send(handler, new SetRegionCommand(regions));
                    break;
                case CommandType.GetIndicator:
                    var indicators = DataBaseManager.IndicatorJsonRecordMap.Items.ToDictionary(region => region.id_indicator, region => region.name_indicator);
                    AsynchronousSocketListener.Send(handler, new SetIndicatorCommand(indicators));
                    break;
                case CommandType.GetData:
                    var dataItems = DataBaseManager.DataJsonRecordMap.Items.Select(dataItem => new SetDataItem()
                    {
                        id_value = dataItem.id_value, id_region = dataItem.id_region, id_indicator = dataItem.id_indicator, data = dataItem.data, value = dataItem.value
                    }).ToList();
                    AsynchronousSocketListener.Send(handler, new SetDataCommand(dataItems));
                    break;
            }
        }
    }
}
