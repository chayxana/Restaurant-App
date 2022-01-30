import * as React from 'react';
import { IFoodDto } from "src/api/dtos/FoodDto";
import FoodsList from './FoodsList';
import { RootState } from 'src/store/reducers/redusers';
import { connect } from 'react-redux';
import { getFoods } from 'src/store/reducers/foods';
import * as foods from 'src/store/actions/foods';
import { NavigateFunction, useNavigate } from 'react-router';

interface StateProps {
	foods: IFoodDto[];
	navigate: NavigateFunction;
}

interface DispatchProps {
	fetchFoods(): void;
}

class FoodsListContainer extends React.PureComponent<StateProps & DispatchProps> {
	public componentDidMount() {
		this.props.fetchFoods();
	}

	public render() {
		return <FoodsList {...this.props} />;
	}
}

const mapStateToProps = (state: RootState): StateProps => ({
	foods: getFoods(state.foods),
	navigate : useNavigate()
});

const mapDispatchToProps = {
	fetchFoods: foods.fetchFoods
};

export default connect<StateProps, DispatchProps>(
	mapStateToProps,
	mapDispatchToProps
)(FoodsListContainer);