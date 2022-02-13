function [Hardness] = QueryAnnModel(DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio)
AnnModel = evalin('base', 'AnnModel');
Inputs = [DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio]';
Hardness = AnnModel(Inputs);
