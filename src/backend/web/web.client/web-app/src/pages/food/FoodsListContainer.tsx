import * as React from 'react';
import { IFoodDto } from "src/api/dtos/FoodDto";
import { RouteComponentProps } from 'react-router';
import FoodsList from './FoodsList';
import { RootState } from 'src/store/reducers/redusers';
import { connect } from 'react-redux';
import { getFoods } from 'src/store/reducers/foods';
import * as foods from 'src/store/actions/foods';

type OwnProps = RouteComponentProps<{}>;

interface StateProps {
	foods: IFoodDto[];
}

interface DispatchProps {
	fetchFoods(): void;
}

class FoodsListContainer extends React.PureComponent<StateProps & DispatchProps & OwnProps> {
	
	public componentDidMount() {
		this.props.fetchFoods();
	}

	public render() {
		return <FoodsList {...this.props} />;
	}
}

const mapStateToProps = (state: RootState): StateProps => ({
	foods: getFoods(state.foods)
});

const mapDispatchToProps = {
	fetchFoods: foods.fetchFoods
};

export default connect<StateProps, DispatchProps, OwnProps>(
	mapStateToProps,
	mapDispatchToProps
)(FoodsListContainer);