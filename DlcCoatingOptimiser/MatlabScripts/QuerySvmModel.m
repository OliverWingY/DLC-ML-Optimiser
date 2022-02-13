function [Hardness] = QuerySvmModel(DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio)
SvmModel = evalin('base', 'SvmModel');
Inputs = [DepositionTime, MicrowavePower, WorkingPressure, GasFlowRateRatio];
assignin('base', 'Inputs', Inputs)
Hardness = predict(SvmModel, Inputs);