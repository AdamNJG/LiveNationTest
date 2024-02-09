using FluentAssertions;
using NumberConverter.Converting;
using NumberConverter.Converting.Dto;
using NumberConverter.Converting.Interfaces;
using NumberConverter.Rules;
using NumberConverterTest.Parsing.helpers;
using Moq;

namespace NumberConverterTest.Parsing
{
    public class ConverterServiceTest
    {
        [Fact]
        public void Parse_OneToTwenty_DefaultRules()
        {
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(1, 20);

            result.result.Should().Be("1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation");
            result.summary["Integer"].Should().Be(11);
            result.summary["LiveNation"].Should().Be(1);
            result.summary["Nation"].Should().Be(3);
            result.summary["Live"].Should().Be(5);
        }


        // I've added this to restrict the inputs for the moment, if we want to support negative numbers that can be added in later
        // At the moment it wasn't mentioned in the brief, I would usually have a chat to discuss this
        [Fact]
        public void Parse_Negative_ReturnsError()
        {
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(-1, 20);

            result.result.Should().Be("Integers may not be negative");
            result.summary["Error"].Should().Be(1);
        }

        [Fact]
        public void Parse_DefaultRulesNoMatch_NumbersReturned()
        {
            int input1 = 1;
            int input2 = 2;
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(input1, input2);

            result.result.Should().Be("1 2");
            result.summary["Integer"].Should().Be(2);
        }

        [Fact]
        public void Parse_DefaultRules_Matches3()
        {
            int input1 = 1;
            int input2 = 3;
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(input1, input2);

            result.result.Should().Be("1 2 Live");
            result.summary["Integer"].Should().Be(2);
            result.summary["Live"].Should().Be(1);
        }

        [Fact]
        public void Parse_DefaultRules_Matches5()
        {
            int input1 = 1;
            int input2 = 5;
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(input1, input2);

            result.result.Should().Be("1 2 Live 4 Nation");
            result.summary["Integer"].Should().Be(3);
            result.summary["Live"].Should().Be(1);
            result.summary["Nation"].Should().Be(1);
        }

        [Fact]
        public void Parse_DefaultRules_15LiveNation()
        {
            int input1 = 1;
            int input2 = 15;
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            mockStore
                .Setup(d => d.GetRules())
                .Returns(ConverterServiceHelper.DefaultRules());

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(input1, input2);

            result.result.Should().Be("1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation");
            result.summary["Integer"].Should().Be(8);
            result.summary["Live"].Should().Be(4);
            result.summary["Nation"].Should().Be(2);
            result.summary["LiveNation"].Should().Be(1);
        }

        [Fact]
        public void Parse_CustomRules_Matches()
        {
            int input1 = 1;
            int input2 = 13;
            Mock<IConverterRuleStore> mockStore = new Mock<IConverterRuleStore>();
            List<RuleDto> rules = ConverterServiceHelper.DefaultRules();
            rules.Add(new RuleDto()
            {
                RuleOperator = RuleOperator.Addition, 
                Operand = 0,
                ExpectedResult = 13,
                ResultString = "THIRTEEN!"
            });

            mockStore
                .Setup(d => d.GetRules())
                .Returns(rules);

            ConverterService service = new ConverterService(mockStore.Object);

            ConverterResult result = service.Parse(input1, input2);

            result.result.Should().Be("1 2 Live 4 Nation Live 7 8 Live Nation 11 Live THIRTEEN!");
            result.summary["Integer"].Should().Be(6);
            result.summary["Live"].Should().Be(4);
            result.summary["Nation"].Should().Be(2);
            result.summary["THIRTEEN!"].Should().Be(1);
        }
    }
}
